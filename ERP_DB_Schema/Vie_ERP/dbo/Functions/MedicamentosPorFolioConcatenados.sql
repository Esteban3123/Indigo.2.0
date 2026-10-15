
CREATE FUNCTION [dbo].[MedicamentosPorFolioConcatenados](@CodigoPaciente as varchar(25), @NumIngreso as char(10), @NumFolio as varchar(10))
RETURNS varchar(max)
AS
BEGIN
    DECLARE @Medicamentos as varchar(max)

SELECT 
	@Medicamentos = STRING_AGG(
						CONCAT(	RTRIM(B.DESPRODUC), ' - ',
						RTRIM(CASE WHEN A.FORMAPRESCRIBE IS NOT NULL THEN A.DESADMINI ELSE CASE WHEN A.DOSISPRFN IS NULL THEN A.DESADMINI WHEN A.DURACIDOS='Dosis Unica' THEN RTRIM(CAST(A.DOSISPRFN AS CHAR)) + ' '  
						+ RTRIM(CAST(C.ABRUNIMED AS CHAR)) + ' Dosis Única Via: ' + RTRIM(D.DESVIAADM) ELSE RTRIM(CAST(A.DOSISPRFN AS CHAR)) + ' ' + RTRIM(CAST(C.ABRUNIMED AS CHAR)) + ' Cada ' +  
						RTRIM(CAST(A.FRECUENCI AS CHAR)) + CASE A.UNIFRECUE WHEN '1' THEN ' min(s) ' WHEN '2' THEN ' Hora(s) ' WHEN '3' THEN ' Dia(s) ' END + 'Vía: ' + RTRIM(D.DESVIAADM) END END)), ', ')
        FROM 
            HCPRESCRD A With(Nolock) 
            INNER JOIN IHLISTPRO B With(Nolock) On A.CODPRODUC=B.CODPRODUC
            INNER JOIN HCVIAADMI D With(Nolock) On A.CODVIAADM=D.CODVIAADM 
            LEFT OUTER JOIN INUNIMEDI C With(Nolock) On A.CODUNIMFN=C.CODUNIMED 
        WHERE 
            A.IPCODPACI= @CodigoPaciente AND NUMINGRES = @NumIngreso AND A.MANEXTPRO IN('0','1') AND A.IDETIPHIS<>'CODIGOAZU' AND IDESQUEMAONC IS NULL AND A.NUMEFOLIO= @NumFolio  
    
    RETURN @Medicamentos
END