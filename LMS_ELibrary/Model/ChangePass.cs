namespace LMS_ELibrary.Model
{
    public class ChangePass
    {
        public ChangePass(string password, string newPass, string reNewass)
        {
            Password = password;
            this.newPass = newPass;
            this.reNewass = reNewass;
        }

        public string Password { get; set; }
        public string newPass { get; set; }
        public string reNewass { get; set; }
    }
}
