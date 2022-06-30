using WMS.Moudle.Utility.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static WMS.Moudle.Utility.StaticParams;

namespace WMS.Moudle.Utility.Serveice
{
    internal class JwtHelper : IJwtHelper
    {
        public string GetJwtToken(JWTOption jWTOption, Claim[] claims)
        {
            //生成加密key
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWTOption.SecurityKey));
            //生成Credentials
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            //生成token
            JwtSecurityToken token = new JwtSecurityToken(
                    jWTOption.Issuer,                                   //发布者
                    jWTOption.Audience,                                 //使用者
                    claims,
                    DateTime.Now,                                       //颁发时间
                    DateTime.Now.AddMinutes(jWTOption.ExpiresMinutes),  //有效期
                    signingCredentials                                  //加密凭证
                    );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
