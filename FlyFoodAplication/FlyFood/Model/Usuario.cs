namespace FlyFood.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public Usuario(){
            
        }

        public Usuario(string email, string senha, int id){
            this.Email = email;
            this.Senha = senha;
            this.Id = id;
        }
    }
}