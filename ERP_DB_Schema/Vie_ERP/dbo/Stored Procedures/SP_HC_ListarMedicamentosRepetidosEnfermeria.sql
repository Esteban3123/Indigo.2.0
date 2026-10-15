
CREATE  PROCEDURE [dbo].[SP_HC_ListarMedicamentosRepetidosEnfermeria]
(
@Paciente varchar(25),
@CodigoMedicamento char(15),
@OpcionConsulta as int, -- 1:Programacion Medicamentos ; 2: Aplicación Medicamentos
@IDHCORDQUIMIO AS INT,
@CONSECPRESCRA AS INT
)
AS
BEGIN
	SET NOCOUNT ON;

if @OpcionConsulta = 1 begin

		select DISTINCT  A.FECHAREGISTRO AS 'Fecha Orden',Rtrim(C.Description) as 'Nombre Esquema',Rtrim(d.NOMMEDICO) as 'Nombre medico',Rtrim(b.INSTRUADMINIS) as 'Instrucciones',
		case B.MEDICAMENTOENCASA WHEN 1 THEN 'Ordenado para Administración domiciliaria' ELSE dbo.[FechasAplicacionMedicamentoOncologico] (1,@Paciente,@CodigoMedicamento, B.IDHCORDQUIMIO,1)  END AS 'Ultima Fecha Aplicacion',
		case B.MEDICAMENTOENCASA WHEN 1 THEN 'Ordenado para Administración domiciliaria' ELSE dbo.[FechasAplicacionMedicamentoOncologico] (2,@Paciente,@CodigoMedicamento,B.IDHCORDQUIMIO,1)  END AS 'Proxima Fecha Aplicacion'
		from [EHR].[HCORDQUIMIO] A
			INNER JOIN [EHR].Schemes c ON A.SchemesId = c.Id
			INNER JOIN [EHR].[HCORDMEDICAM] B ON A.ID = B.IDHCORDQUIMIO
			INNER JOIN dbo.INPROFSAL d ON d.CODPROSAL = A.CODPROSAL
		WHERE A.IPCODPACI = @Paciente AND A.ESTADO IN (1,2) AND B.CODPRODUC = @CodigoMedicamento and EXISTS  (select IDHCORDQUIMIO from HCHOJAMED  WHERE  MEDESTADO = 1 AND IDHCORDQUIMIO = A.ID )

end
else if @OpcionConsulta = 2 begin

		select DISTINCT B.ID, B.FECHAREGISTRO AS 'Fecha Orden',Rtrim(d.NOMMEDICO) as 'Nombre medico',Rtrim(A.INSTRUADMINIS) as 'Instrucciones' ,
			case A.MEDICAMENTOENCASA WHEN 1 THEN 'Ordenado para Administración domiciliaria' ELSE dbo.[FechasAplicacionMedicamentoOncologico] (1,@Paciente,@CodigoMedicamento, A.IDHCORDQUIMIO,2)  END AS 'Ultima Fecha Aplicacion',
			case A.MEDICAMENTOENCASA WHEN 1 THEN 'Ordenado para Administración domiciliaria' ELSE dbo.[FechasAplicacionMedicamentoOncologico] (2,@Paciente,@CodigoMedicamento,A.IDHCORDQUIMIO,2)  END AS 'Proxima Fecha Aplicacion'
		from [EHR].[HCORDMEDICAM] A 
		Inner Join [EHR].[HCORDQUIMIO] B ON A.IDHCORDQUIMIO = B.ID
		INNER JOIN dbo.INPROFSAL d ON d.CODPROSAL = B.CODPROSAL
		WHERE  B.IPCODPACI = @Paciente AND B.ESTADO IN (1,2) AND A.CODPRODUC = @CodigoMedicamento AND B.ID <> @IDHCORDQUIMIO and EXISTS  (select IDHCORDQUIMIO from HCHOJAMED  WHERE  MEDESTADO = 1 AND IDHCORDQUIMIO = B.ID )
		
end

end