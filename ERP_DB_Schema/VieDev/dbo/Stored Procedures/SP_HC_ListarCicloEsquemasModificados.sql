-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarCicloEsquemasModificados]
(
@CentroAtencion char(20),
@UnidadFuncional char(20),
@FechaInicial datetime,
@FechaFinal datetime
)
AS
BEGIN
    SET NOCOUNT ON

	
SELECT   S.FECHAREGISTRO as 'fechaModificacion', S.IPCODPACI as 'identificacion', RTRIM(I.IPNOMCOMP) as 'paciente',
		[dbo].[Edad](convert(date,IPFECNACI),convert(date,getdate())) as 'edad' , 
		Rtrim(b.NOMENTIDA) as 'entidad', S.NUMINGRES as 'ingreso', RTRIM(sch.Code) +'-'+ RTRIM(sch.Description) as 'nombreEsquema',
		S.CICLO as 'ciclo', RTRIM(diag.CODDIAGNO)+'-'+ RTRIM(diag.NOMDIAGNO) as 'diagnostico', Rtrim(G.CODMOTANU)+'-'+ Rtrim(G.DESMOTANU) as 'motivoModificacion',
		Rtrim(S.OBSERVACIONMOD) as 'observacion', RTRIM(K.NOMMEDICO) as 'medico', rtrim(p.DESESPECI) as 'especialidad',
		CASE I.IPSEXOPAC WHEN 1 THEN 'Masculino' WHEN 2 THEN 'Femenino' END  AS 'genero'
FROM EHR.HCORDCICLOS S
	inner join EHR.HCORDQUIMIO c on S.IDHCORDQUIMIO = c.ID 
	inner join EHR.Schemes sch on sch.id = c.SchemesId
	inner join dbo.INDIAGNOS diag on diag.CODDIAGNO = c.CODDIAGNO
	inner join dbo.HCMOANULB G on G.CODMOTANU = S.IDHCMOANULB
	inner join dbo.INPROFSAL K on K.CODPROSAL= S.CODPROSAL
	inner join dbo.INESPECIA p on p.CODESPECI= S.CODESPECI
	inner join dbo.INPACIENT I on I.IPCODPACI  = S.IPCODPACI 
	inner join dbo.ADINGRESO H on H.NUMINGRES = S.NUMINGRES 
	inner join dbo.INENTIDAD b on b.CODENTIDA = H.CODENTIDA
WHERE (s.IDHCMOANULB IS NOT NULL  AND s.OBSERVACIONMOD IS NOT NULL) 
	   and S.CODCENATE = @CentroAtencion AND S.UFUCODIGO = @UnidadFuncional AND S.FECHAREGISTRO between @FechaInicial AND @FechaFinal 
END