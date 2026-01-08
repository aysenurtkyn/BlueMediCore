namespace BlueMediCore.DTOs
{
    // <T>: Buraya "T" yerine ne gönderirsek (Doktor, Hasta) ona dönüşecek demektir.
    public class ServiceResponse<T>
    {
        public T? Data { get; set; } // Asıl veri (Doktor listesi vb.)
        public bool Success { get; set; } = true; // Başarılı mı?
        public string Message { get; set; } = string.Empty; // Bilgi mesajı

        // Hata durumunda dönecek fonksiyon
        public static ServiceResponse<T> Fail(string message)
        {
            return new ServiceResponse<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }

        // Başarılı durumda dönecek fonksiyon
        public static ServiceResponse<T> Ok(T data, string message = "İşlem başarılı.")
        {
            return new ServiceResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }
    }
}