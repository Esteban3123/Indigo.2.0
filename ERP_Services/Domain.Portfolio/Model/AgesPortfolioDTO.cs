#region "Usings"
using System;
#endregion

namespace Domain.Portfolio.Model
{
    /// <summary>
    /// Clase auxiliar para manejar y transferir los datos de Edades de Cartera
    /// </summary>
    public class AgesPortfolioDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte RangeType { get; set; }

    }
}
