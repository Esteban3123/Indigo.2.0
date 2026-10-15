CREATE VIEW [dbo].[I_HC_OrdenesLaboratorioCovid]
AS
SELECT O.FECORDMED AS FechaOrden, O.IPCODPACI AS Identificación, O.NUMINGRES AS Ingreso, P.IPNOMCOMP AS Paciente, O.CODSERIPS AS CodigoServicio, 
             CASE O.ESTSERIPS WHEN 1 THEN 'Solicitado' WHEN 2 THEN 'MuestraRecolectada' WHEN 3 THEN 'ResultadoEntregado' WHEN 4 THEN 'ExamenInterpretado' WHEN 5 THEN 'Remitido' WHEN 6 THEN 'Anulado' WHEN 7 THEN 'Extramural' WHEN 8 THEN 'MuestraParcial' END AS Estado, 
             U.UFUDESCRI AS Unidad, GA.Name AS GrupoAtención, M.NOMMEDICO AS MedicoSolicita
FROM   dbo.HCORDLABO AS O INNER JOIN
             dbo.INPACIENT AS P ON O.IPCODPACI = P.IPCODPACI AND CONVERT(nvarchar(10), O.FECORDMED, 20) > CONVERT(nvarchar(10), GETDATE() - 182, 20) RIGHT OUTER JOIN
             dbo.INUNIFUNC AS U ON O.UFUCODIGO = U.UFUCODIGO INNER JOIN
             dbo.INPROFSAL AS M ON O.CODPROSAL = M.CODPROSAL INNER JOIN
             dbo.ADINGRESO AS H ON O.NUMINGRES = H.NUMINGRES INNER JOIN
             Contract.CareGroup AS GA ON H.GENCAREGROUP = GA.Id
WHERE (O.CODSERIPS = '908856')