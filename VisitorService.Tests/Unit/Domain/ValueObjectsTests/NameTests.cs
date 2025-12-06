using VisitorService.Domain.ValueObject;

namespace VisitorService.Domain.Tests.NameTests
{
    public class NameTests
    {
        [Fact]
        public void should_add_notifications_for_empty_fields()
        {
            var name = Name.Create(" ");

            Assert.True(name.HasErrors);
            Assert.Contains( name.Notification, n => n.Message == "O nome não pode ser vazio ou apenas espaços");
        }

        [Fact]
        public void should_add_notifications_with_fewer_than_2_characters()
        {
            var name = Name.Create("o");

            Assert.True(name.HasErrors);
            Assert.Contains(name.Notification, n => n.Message == "O nome deve ter pelo menos 2 caracteres.");
        }
        [Fact]
        public void should_add_notifications_with_fewer_than_100_characters()
        {
            var name = Name.Create("Apesar de todas as dificuldades que surgem ao longo do caminho quando alguém decide realmente se dedicar a estudar programação e compreender, de maneira profunda, a lógica que sustenta cada tecnologia, cada linguagem e cada padrão arquitetural, é justamente esse processo contínuo de descoberta, tentativa, erro, refatoração e aprendizado constante que acaba formando não apenas um profissional mais preparado, mas também uma pessoa mais resiliente, capaz de analisar problemas complexos com clareza, propor soluções melhores e, principalmente, manter a disciplina necessária para evoluir mesmo quando os resultados demoram um pouco mais para aparecer.");

            Assert.True(name.HasErrors);
            Assert.Contains(name.Notification, n => n.Message == "O nome deve ter no máximo 100 caracteres.");
        }
    }
}