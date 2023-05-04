using System.ComponentModel;

namespace SistemaDeTarefas.Enums
{
    public enum StatusTarefa
    {
        [Description("Pendente")]
        Pendente = 1,
        [Description("Andamento")]
        Andamento = 2,
        [Description("Concluido")]
        Concluido = 3
    }
}
