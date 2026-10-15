
CREATE VIEW [dbo].[ViewListadoServiciosIPS_ProcedimientosInterconsultas]
AS
SELECT        RTRIM(CODSERIPS) AS Codigo, RTRIM(DESSERIPS) AS Servicio, TIPSERTER AS Terapia, RTRIM(CODSERIPS) + ' - ' + RTRIM(DESSERIPS) AS CodigoDescripcion, SERREASIT AS ServicioRealizaSitio, 
                         IPSSERIAD AS ServicioSeriado, SERIPSDASH, TIPSERIPS
FROM            dbo.INCUPSIPS
WHERE        (TIPSERIPS = '6') AND (SIPSESTADO = '1')