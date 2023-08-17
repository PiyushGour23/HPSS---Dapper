namespace Hpss.Model
{
    public class APIResponse
    {
        public int ResponseCode { get; set; }
        public string ErrorMessage { get; set; } = null!;
        public string Result { get; set; } = null!;
    }
}
