using System.ComponentModel.DataAnnotations;

namespace WMS.Moudle.Entity.Dto.System
{
    /// <summary>
    /// 用户入参(用户账号不自持编辑)
    /// </summary>
    public class UserDto:SystemDto
    {
        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(20, ErrorMessage = "名称最大长度:20")]
        public string password { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [MaxLength(20, ErrorMessage = "姓名最大长度:20")]
        public string real_name { get; set; }

        /// <summary>
        /// 部门id
        /// </summary>
        public long dept_id { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public long role_id { get; set; }

        /// <summary>
        /// 入库特殊权限
        /// </summary>
        public int power_state { get; set; }

    }
}
