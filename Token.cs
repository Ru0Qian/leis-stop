using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Auto;

    class Token
    {
        private static readonly HttpClient client = new();
        private static async Task<Tuple<bool, string>> Login(string username, string password)
        {
            string loginUrl = "https://webapi.leigod.com/api/auth/login";
            var loginData = new
            {
                username = username,
                password = GenerateMD5(password),
                user_type = "0",
                src_channel = "guanwang",
                country_code = 86,
                lang = "zh_CN",
                region_code = 1,
                account_token = "null"
            };

            var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync(loginUrl, content);
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(responseBody);

                if (result?.code == 0)
                {
                    string token = result.data.login_info.account_token;
                    return Tuple.Create(true, token);
                }
                else
                {
                    return Tuple.Create(false, (string)result.msg);
                }
            }
            catch (Exception ex)
            {
                return Tuple.Create(false, "请求失败: " + ex.Message);
            }
        }

        public static async Task<Tuple<bool, string>> GetToken(string username, string password)
        {
            var loginResult = await Login(username, password);
            if (loginResult.Item1)
            {
                string token = loginResult.Item2;
                // 假设使用某种配置管理工具保存 token
                // ConfigManager.Set("account_token", token);
                return Tuple.Create(true, "Token 获取成功: " + token);
            }
            else
            {
                return Tuple.Create(false, loginResult.Item2);
            }
        }

        private static string GenerateMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
