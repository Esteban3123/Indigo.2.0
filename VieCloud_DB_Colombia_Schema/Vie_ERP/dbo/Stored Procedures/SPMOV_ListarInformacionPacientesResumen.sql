
CREATE Procedure [dbo].[SPMOV_ListarInformacionPacientesResumen]
(
 @Paciente varchar(25),
 @Ingreso varchar(50)
)
AS
Select  RTRIM(B.IPNOMCOMP) as 'Paciente',
        B.IPFECNACI as 'FechaNacimiento',
		SUBSTRING (dbo.ObtenerFechaFormateada(B.IPFECNACI) ,0 , 11)  as 'FechaFormateada',
		EdadFormateada = '',
		RTRIM(B.CODIGONIT) as 'Identificacion',
		CASE
			when B.IPSEXOPAC = 1 then 'Masculino'
			when B.IPSEXOPAC = 2 then 'Femenino'
			else ' '
		END as 'Sexo',
		CASE
			when B.IPGRUPSAN is null then 'No registra Grupo Sanguineo'
			else B.IPGRUPSAN
		END as 'GrupoSanguineo',
		CASE
			when B.IPRHSANGR is null then 'No registra Rh'
			else B.IPRHSANGR
		END as 'RhSanguineo',
		RTRIM(B.IPDIRECCI) as 'Direccion',
		RTRIM(B.IPTELEFON) as 'Fijo',
		RTRIM(B.IPTELMOVI) as 'Movil',
		RTRIM(C.NOMDIAGNO) as 'Diagnostico'
		-- Alertas por Medicamentos //PARTE DE ALERGIAS A MEDICAMENTOS
		--RTRIM(E.DESPRODUC) AS 'Producto',
		--RTRIM(D.MOTSUSMED) AS 'MotivoSuspencion',
		--RTRIM(D.CODPRODUC) AS 'CodigoProducto',
		--RTRIM(d.NUMEFOLIO) AS 'Folio',
		--Escalas de Riesgo //PARTE DE ESCALAS DE RIESGO
		--CASE 
		--	when RTRIM(ESCADOWNT) = 1 then 'Si'
		--	when RTRIM(ESCADOWNT) = 0 then 'No'
		--	when RTRIM(ESCADOWNT) is null then 'No'
		--end as 'DownTon',
		--CASE 
		--	when RTRIM(ESCABIERI) = 1 then 'Si'
		--	when RTRIM(ESCABIERI) = 0 then 'No'
		--	when RTRIM(ESCABIERI) is null then 'No'
		--end as 'Bieri',
		--CASE 
		--	when RTRIM(ESCARASS) = 1 then 'Si'
		--	when RTRIM(ESCARASS) = 0 then 'No'
		--	when RTRIM(ESCARASS) is null then 'No'
		--end as 'RASS',
		--CASE 
		--	when RTRIM(ESCNORPAC) = 1 then 'Si'
		--	when RTRIM(ESCNORPAC) = 0 then 'No'
		--	when RTRIM(ESCNORPAC) is null then 'No'
		--end as 'NorTon',
		--CASE 
		--	when RTRIM(ESCVASPAC) = 1 then 'Si'
		--	when RTRIM(ESCVASPAC) = 0 then 'No'
		--	when RTRIM(ESCVASPAC) is null then 'No'
		--end as 'VAS',
		--CASE 
		--	when RTRIM(ESCAPAPAC) = 1 then 'Si'
		--	when RTRIM(ESCAPAPAC) = 0 then 'No'
		--	when RTRIM(ESCAPAPAC) is null then 'No'
		--end as 'Apache'
		-- PARTE QUE REPRESENTA LOS ANTECEDENTES
		--F.ANTMEDPAC as 'Medicos',
		--F.ANTQUIPAC as 'Quirurgicos',	
		--F.ANTTRAPAC as 'Transfucionales',
		--F.ANTINMPAC as 'Inmunologicos',
		--F.ANTALEPAC as 'Alergicos',
		--F.ANTTRUPAC as 'Traumaticos',	
		--F.ANTPSIPAC as 'Psicologicos&Psiquiatricos',
		--F.ANTFARPAC as 'Farmacologicos',
		--F.ANTFAMPAC as 'Familiares',
		--F.ANTTOXPAC as 'Toxicos',
		--F.ANTOTRPAC as 'Otros'
from ADINGRESO A
inner join INPACIENT B on B.IPCODPACI = A.IPCODPACI
inner join INDIAGNOS C on C.CODDIAGNO =  (SELECT TOP 1 CODDIAGNO FROM INDIAGNOP WHERE IPCODPACI = B.IPCODPACI AND NUMINGRES = A.NUMINGRES AND CODDIAPRI = 1)
--left JOIN HCMEDRIES D ON D.IPCODPACI = A.IPCODPACI AND D.NUMINGRES = A.NUMINGRES
--left join IHLISTPRO E on E.CODPRODUC = D.CODPRODUC
--left join HCANTPACH F on F.IPCODPACI = B.IPCODPACI
where A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Retorna un resumen clínico y demográfico de un paciente para un ingreso (episodio de atención) específico. Combina datos personales del paciente (nombre completo, fecha de nacimiento, cédula o identificación, sexo, grupo sanguíneo, RH, dirección y teléfonos) con el diagnóstico principal CIE-10 registrado para ese ingreso. Para ello cruza el registro de ingreso (ADINGRESO), la ficha maestra del paciente (INPACIENT), el diagnóstico principal del episodio (INDIAGNOP) y el catálogo de diagnósticos clínicos (INDIAGNOS). Se usa típicamente en la pantalla de resumen o carátula del paciente durante la atención hospitalaria, urgencias o consulta, recibiendo como filtros el código del paciente y el número de ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarInformacionPacientesResumen';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarInformacionPacientesResumen';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve un resumen demográfico y clínico básico del paciente (datos personales, contacto y diagnóstico principal) asociado a un ingreso específico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarInformacionPacientesResumen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro de ingreso que coincida con el paciente y número de ingreso indicados.; El paciente debe tener al menos un diagnóstico marcado como principal (CODDIAPRI=1) para el ingreso, de lo contrario el INNER JOIN excluye el registro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarInformacionPacientesResumen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera el diagnóstico marcado como principal (CODDIAPRI=1) y, en caso de múltiples, únicamente el primero (TOP 1).; Los campos de texto se devuelven sin espacios a la derecha (RTRIM).; La fecha de nacimiento se entrega además en formato corto de 10 caracteres mediante ObtenerFechaFormateada.; El campo EdadFormateada siempre se devuelve vacío.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarInformacionPacientesResumen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Diagnóstico principal; Grupo sanguíneo y Rh; Identificación (NIT); Datos demográficos y de contacto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarInformacionPacientesResumen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Retorna una fila con datos del paciente y diagnóstico principal cuando existe coincidencia de paciente e ingreso y un diagnóstico con CODDIAPRI=1.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarInformacionPacientesResumen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPSEXOPAC = 1 → Sexo se reporta como ''Masculino'' else Si IPSEXOPAC = 2 se reporta ''Femenino''; en otro caso espacio en blanco.; si IPGRUPSAN IS NULL → Devuelve ''No registra Grupo Sanguineo'' else Devuelve el valor del grupo sanguíneo registrado.; si IPRHSANGR IS NULL → Devuelve ''No registra Rh'' else Devuelve el valor del Rh registrado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarInformacionPacientesResumen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ObtenerFechaFormateada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarInformacionPacientesResumen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; dbo.INDIAGNOS; dbo.INDIAGNOP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarInformacionPacientesResumen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarInformacionPacientesResumen';
-- GO
