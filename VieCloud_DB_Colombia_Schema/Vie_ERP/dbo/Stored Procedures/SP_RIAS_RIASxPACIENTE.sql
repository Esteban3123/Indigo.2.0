
-- =============================================
-- Author:		Rafael Eduardo Patiño
-- Create date: 18/11/2018
-- Description:	retorna las RIAS que apliquen para la edad del paciente
-- =============================================
CREATE PROCEDURE [dbo].[SP_RIAS_RIASxPACIENTE]
(
  @Identificacion as varchar(25)
)
AS
BEGIN
  SET NOCOUNT ON;
        
     declare @Sexo as integer
	 declare @FechaNacimiento as datetime
	 declare @TableRIAS table(ID INTEGER NOT NULL,    CODIGO varchar(5) NOT NULL, NOMBRE varchar(150) NOT NULL)

	 select @Sexo = IPSEXOPAC,@FechaNacimiento = IPFECNACI from INPACIENT where IPCODPACI = @Identificacion	 
	 declare @EdadAnos as integer = datediff (YEAR  , @FechaNacimiento , [Common].[GETDATE]() ) 
	 declare @EdadMeses as integer= datediff (MONTH , @FechaNacimiento,[Common].[GETDATE]() ) 
	 declare @Edaddias as integer= datediff (DAY , @FechaNacimiento,[Common].[GETDATE]() ) 	 
	 
	 --Rias aplican x años
	 insert into @TableRIAS 
	 select ID,CODPRO,NOMBRE  from RIAS R  where (@EdadAnos BETWEEN EDADMINIMA AND EDADMAXIMA ) AND TIPOEDAD = 2 AND SEXO IN(@Sexo,3) AND ESTADO = 1	 

	 --Rias aplican x meses
	 insert into @TableRIAS 
	 select ID,CODPRO,NOMBRE from RIAS R  where (@EdadMeses BETWEEN EDADMINIMA AND EDADMAXIMA ) AND TIPOEDAD = 1 AND SEXO IN(@Sexo,3) AND ESTADO = 1	 	 

	 --Rias aplican x dias
	 insert into @TableRIAS 
	 select ID,CODPRO,NOMBRE from RIAS R  where (@Edaddias BETWEEN EDADMINIMA AND EDADMAXIMA ) AND TIPOEDAD = 0 AND SEXO IN(@Sexo,3) AND ESTADO = 1
	 
	 --se listan solo las Rias a las que no esta ya inscrito el paciente
	 select ID,CODIGO AS 'Codigo',NOMBRE as 'Descripcion' from @TableRIAS where ID not in (select IDRIAS from RIASXPACIENTE where IPCODPACI = @Identificacion AND ESTADO = 2 )
	   
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dado el número de identificación (cédula) de un paciente, devuelve las Rutas Integrales de Atención en Salud (RIAS) que le aplican según su edad actual (calculada en años, meses o días) y su sexo, excluyendo aquellas rutas en las que el paciente ya se encuentra inscrito y activo. Consulta los datos demográficos del paciente en INPACIENT, evalúa los criterios de elegibilidad definidos en el catálogo RIAS, y filtra las ya enroladas revisando RIASXPACIENTE. Se usa para presentarle al operador o al sistema cuáles programas de salud preventiva están disponibles para inscribir al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_RIAS_RIASxPACIENTE';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_RIAS_RIASxPACIENTE';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las Rutas Integrales de Atención en Salud (RIAS) aplicables a un paciente según su edad y sexo, excluyendo aquellas en las que ya está inscrito activamente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_RIASxPACIENTE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en INPACIENT con sexo y fecha de nacimiento registrados.; Las RIAS deben tener configurados rangos de edad (EDADMINIMA, EDADMAXIMA), TIPOEDAD (0=días, 1=meses, 2=años), SEXO y ESTADO.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_RIASxPACIENTE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera RIAS con ESTADO = 1 (activas).; El valor SEXO=3 en RIAS representa aplicabilidad a cualquier sexo.; TIPOEDAD determina la unidad temporal: 0=días, 1=meses, 2=años.; La edad se calcula contra la fecha actual obtenida vía Common.GETDATE().; No se devuelven RIAS donde el paciente ya tenga inscripción con ESTADO=2.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_RIASxPACIENTE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; RIAS (Rutas Integrales de Atención en Salud); Edad del paciente; Sexo; Inscripción a RIAS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_RIASxPACIENTE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve ID, Código y Descripción de las RIAS cuya edad/sexo aplican al paciente y a las que no está inscrito con ESTADO=2 en RIASXPACIENTE.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_RIASxPACIENTE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPOEDAD = 2 y edad en años del paciente entre EDADMINIMA y EDADMAXIMA → Incluye la RIA evaluada por años; si TIPOEDAD = 1 y edad en meses del paciente entre EDADMINIMA y EDADMAXIMA → Incluye la RIA evaluada por meses; si TIPOEDAD = 0 y edad en días del paciente entre EDADMINIMA y EDADMAXIMA → Incluye la RIA evaluada por días; si SEXO de la RIA coincide con el del paciente o es 3 (ambos/unisex) → La RIA es candidata; si RIA ya registrada en RIASXPACIENTE con ESTADO=2 para el paciente → Se excluye del resultado final', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_RIASxPACIENTE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_RIASxPACIENTE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.RIAS; dbo.RIASXPACIENTE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_RIASxPACIENTE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_RIASxPACIENTE';
-- GO
