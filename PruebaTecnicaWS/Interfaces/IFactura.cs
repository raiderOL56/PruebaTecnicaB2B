using PruebaTecnicaWS.Models;

namespace PruebaTecnicaWS.Interfaces
{
    public interface IFactura
    {
        double Precio { get; set; }
        Task<FacturaGeneral> GetFacturaGeneral(string userID);
        void UpdatePrice(double precio);
    }
}