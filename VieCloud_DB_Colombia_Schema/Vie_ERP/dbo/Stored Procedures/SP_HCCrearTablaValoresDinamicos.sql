
/*-----Sp para crear las tablas de valores dinamicos
Fecha: 25/09/2017
AUtor: Rafael Patiño
*/
CREATE PROCEDURE [dbo].[SP_HCCrearTablaValoresDinamicos]
(
@AnoMes varchar(100),
@Tipo integer
)
AS
BEGIN
	SET NOCOUNT ON;

	--SELECT count(*) FROM sysobjects  WHERE xtype='u' AND name
	
	declare @TablaOT as varchar(50) = N'OTVALORES' + @AnoMes  + ''
	declare @TablaRS as varchar(50) = N'RSVALORES' + @AnoMes  + ''
	declare @TablaANT as nvarchar(50) = N'ANTVALORES' + @AnoMes  + ''
	declare @TablaEXA as nvarchar(50) = N'EXAVALORES' + @AnoMes  + ''

	if not exists (SELECT * FROM sysobjects  WHERE xtype='u' AND name = @TablaOT ) begin			
		declare @SqltablaOtros as nvarchar(max)
		set @SqltablaOtros  = N'SELECT TOP 0 * INTO OTVALORES' + @AnoMes + '  FROM OTVALORES'

		--ejecutamos la creacion de las tablas dinamicas
		EXEC sp_executesql @SqltablaOtros

		set @SqltablaOtros  = N'			
						ALTER TABLE [dbo].[OTVALORES' + @AnoMes + '] 
						ADD CONSTRAINT PK_OTVALORES' + @AnoMes + '_ID PRIMARY KEY CLUSTERED (ID);

						ALTER TABLE [dbo].[OTVALORES' + @AnoMes + ']  WITH CHECK ADD  CONSTRAINT [FK_OTVALORES' + @AnoMes + '_HCHISPACA] FOREIGN KEY([IDHCHISPACA])
						REFERENCES [dbo].[HCHISPACA] ([ID])						

						ALTER TABLE [dbo].[OTVALORES' + @AnoMes + '] CHECK CONSTRAINT [FK_OTVALORES' + @AnoMes + '_HCHISPACA]						

						ALTER TABLE [dbo].[OTVALORES' + @AnoMes + ']  WITH CHECK ADD  CONSTRAINT [FK_OTVALORES' + @AnoMes + '_OTGRUPO] FOREIGN KEY([IDGRUPO])
						REFERENCES [dbo].[OTGRUPO] ([ID])						

						ALTER TABLE [dbo].[OTVALORES' + @AnoMes + '] CHECK CONSTRAINT [FK_OTVALORES' + @AnoMes + '_OTGRUPO]						
											
						ALTER TABLE [dbo].[OTVALORES' + @AnoMes + ']  WITH CHECK ADD  CONSTRAINT [FK_OTVALORES' + @AnoMes + '_OTVARIABLES] FOREIGN KEY([IDOTVARIABLE])
						REFERENCES [dbo].[OTVARIABLES] ([ID])
						
						ALTER TABLE [dbo].[OTVALORES' + @AnoMes + '] CHECK CONSTRAINT [FK_OTVALORES' + @AnoMes + '_OTVARIABLES]
					
						ALTER TABLE [dbo].[OTVALORES' + @AnoMes + ']  WITH CHECK ADD  CONSTRAINT [FK_OTVALORES' + @AnoMes + '_OTVARIABLESL] FOREIGN KEY([IDITEMLISTA])
						REFERENCES [dbo].[OTVARIABLESL] ([ID])

						ALTER TABLE [dbo].[OTVALORES' + @AnoMes + '] CHECK CONSTRAINT [FK_OTVALORES' + @AnoMes + '_OTVARIABLESL]				'
		EXEC sp_executesql @SqltablaOtros

	end

	if not exists (SELECT * FROM sysobjects  WHERE xtype='u' AND name = @TablaRS ) begin
		
		
		declare @SqltablaRevisionSistemas as nvarchar(max)
		set @SqltablaRevisionSistemas  = N'SELECT TOP 0 * INTO RSVALORES' + @AnoMes + '  FROM RSVALORES'

		--ejecutamos la creacion de las tablas dinamicas
		EXEC sp_executesql @SqltablaRevisionSistemas

		set @SqltablaRevisionSistemas  = N'			
						ALTER TABLE [dbo].[RSVALORES' + @AnoMes + '] 
						ADD CONSTRAINT PK_RSVALORES' + @AnoMes + '_ID PRIMARY KEY CLUSTERED (ID);

						ALTER TABLE [dbo].[RSVALORES' + @AnoMes + ']  WITH CHECK ADD  CONSTRAINT [FK_RSVALORES' + @AnoMes + '_HCHISPACA] FOREIGN KEY([IDHCHISPACA])
						REFERENCES [dbo].[HCHISPACA] ([ID])						

						ALTER TABLE [dbo].[RSVALORES' + @AnoMes + '] CHECK CONSTRAINT [FK_RSVALORES' + @AnoMes + '_HCHISPACA]						

						ALTER TABLE [dbo].[RSVALORES' + @AnoMes + ']  WITH CHECK ADD  CONSTRAINT [FK_RSVALORES' + @AnoMes + '_RSGRUPO] FOREIGN KEY([IDGRUPO])
						REFERENCES [dbo].[RSGRUPO] ([ID])						

						ALTER TABLE [dbo].[RSVALORES' + @AnoMes + '] CHECK CONSTRAINT [FK_RSVALORES' + @AnoMes + '_RSGRUPO]						
											
						ALTER TABLE [dbo].[RSVALORES' + @AnoMes + ']  WITH CHECK ADD  CONSTRAINT [FK_RSVALORES' + @AnoMes + '_RSVARIABLES] FOREIGN KEY([IDRSVARIABLE])
						REFERENCES [dbo].[RSVARIABLES] ([ID])
						
						ALTER TABLE [dbo].[RSVALORES' + @AnoMes + '] CHECK CONSTRAINT [FK_RSVALORES' + @AnoMes + '_RSVARIABLES]
					
						ALTER TABLE [dbo].[RSVALORES' + @AnoMes + ']  WITH CHECK ADD  CONSTRAINT [FK_RSVALORES' + @AnoMes + '_RSVARIABLESL] FOREIGN KEY([IDITEMLISTA])
						REFERENCES [dbo].[RSVARIABLESL] ([ID])

						ALTER TABLE [dbo].[RSVALORES' + @AnoMes + '] CHECK CONSTRAINT [FK_RSVALORES' + @AnoMes + '_RSVARIABLESL]				'
		EXEC sp_executesql @SqltablaRevisionSistemas

	end

	if not exists (SELECT * FROM sysobjects  WHERE xtype='u' AND name = @TablaANT  ) begin
		
			declare @SqltablaAntecedentes as nvarchar(max)
			set @SqltablaAntecedentes  = N'SELECT TOP 0 * INTO ANTVALORES' + @AnoMes + '  FROM ANTVALORES'
			EXEC sp_executesql @SqltablaAntecedentes

			set @SqltablaAntecedentes = '
									ALTER TABLE [dbo].[ANTVALORES' + @AnoMes + '] 
									ADD CONSTRAINT PK_ANTVALORES' + @AnoMes + '_ID PRIMARY KEY CLUSTERED (ID);
									
									ALTER TABLE [dbo].[ANTVALORES' + @AnoMes + ']  WITH CHECK ADD  CONSTRAINT [FK_ANTVALORES' + @AnoMes + '_ANTVARIABLES] FOREIGN KEY([IDANTVARIABLE])
									REFERENCES [dbo].[ANTVARIABLES] ([ID])
								
									ALTER TABLE [dbo].[ANTVALORES' + @AnoMes + '] CHECK CONSTRAINT [FK_ANTVALORES' + @AnoMes + '_ANTVARIABLES]
								
									ALTER TABLE [dbo].[ANTVALORES' + @AnoMes + ']  WITH CHECK ADD  CONSTRAINT [FK_ANTVALORES' + @AnoMes + '_ANTVARIABLESL] FOREIGN KEY([IDITEMLISTA])
									REFERENCES [dbo].[ANTVARIABLESL] ([ID])
									
									ALTER TABLE [dbo].[ANTVALORES' + @AnoMes + '] CHECK CONSTRAINT [FK_ANTVALORES' + @AnoMes + '_ANTVARIABLESL]
							        
									ALTER TABLE [dbo].[ANTVALORES' + @AnoMes + ']  WITH CHECK ADD  CONSTRAINT [FK_ANTVALORES' + @AnoMes + '_HCHISPACA] FOREIGN KEY([IDHCHISPACA])
									REFERENCES [dbo].[HCHISPACA] ([ID])

									ALTER TABLE [dbo].[ANTVALORES' + @AnoMes + '] CHECK CONSTRAINT [FK_ANTVALORES' + @AnoMes + '_HCHISPACA]

									ALTER TABLE [dbo].[ANTVALORES' + @AnoMes + ']  WITH CHECK ADD  CONSTRAINT [FK_ANTVALORES' + @AnoMes + '_RSVARIABLES] FOREIGN KEY([IDANTVARIABLE])
									REFERENCES [dbo].[ANTVARIABLES] ([ID])

									ALTER TABLE [dbo].[ANTVALORES' + @AnoMes + '] CHECK CONSTRAINT [FK_ANTVALORES' + @AnoMes + '_RSVARIABLES]   '

				EXEC sp_executesql @SqltablaAntecedentes
		
	end 

	if not exists (SELECT * FROM sysobjects  WHERE xtype='u' AND name = @TablaEXA   ) begin

			declare @SqltablaExamenFisico as nvarchar(max)
			set @SqltablaExamenFisico  = N'SELECT TOP 0 * INTO EXAVALORES' + @AnoMes + '  FROM EXAVALORES'
			EXEC sp_executesql @SqltablaExamenFisico

			set @SqltablaExamenFisico  = N'
						ALTER TABLE [dbo].[EXAVALORES' + @AnoMes + '] 
						ADD CONSTRAINT PK_EXAVALORES' + @AnoMes + '_ID PRIMARY KEY CLUSTERED (ID);
		
						ALTER TABLE [dbo].[EXAVALORES' + @AnoMes + ']  WITH CHECK ADD  CONSTRAINT [FK_EXAVALORES' + @AnoMes + '_EXAGRUPO] FOREIGN KEY([IDEXAGRUPO])
						REFERENCES [dbo].[EXAGRUPO] ([ID])
						
						ALTER TABLE [dbo].[EXAVALORES' + @AnoMes + '] CHECK CONSTRAINT [FK_EXAVALORES' + @AnoMes + '_EXAGRUPO]		

						ALTER TABLE [dbo].[EXAVALORES' + @AnoMes + ']  WITH CHECK ADD  CONSTRAINT [FK_EXAVALORES' + @AnoMes + '_EXAVARIABLES] FOREIGN KEY([IDEXAVARIABLE])
						REFERENCES [dbo].[EXAVARIABLES] ([ID])

						ALTER TABLE [dbo].[EXAVALORES' + @AnoMes + '] CHECK CONSTRAINT [FK_EXAVALORES' + @AnoMes + '_EXAVARIABLES]

						ALTER TABLE [dbo].[EXAVALORES' + @AnoMes + ']  WITH CHECK ADD  CONSTRAINT [FK_EXAVALORES' + @AnoMes + '_EXAVARIABLESL] FOREIGN KEY([IDITEMLISTA])
						REFERENCES [dbo].[EXAVARIABLESL] ([ID])

						ALTER TABLE [dbo].[EXAVALORES' + @AnoMes + '] CHECK CONSTRAINT [FK_EXAVALORES' + @AnoMes + '_EXAVARIABLESL]

						ALTER TABLE [dbo].[EXAVALORES' + @AnoMes + ']  WITH CHECK ADD  CONSTRAINT [FK_EXAVALORES' + @AnoMes + '_HCHISPACA] FOREIGN KEY([IDHCHISPACA])
						REFERENCES [dbo].[HCHISPACA] ([ID])
						
						ALTER TABLE [dbo].[EXAVALORES' + @AnoMes + '] CHECK CONSTRAINT [FK_EXAVALORES' + @AnoMes + '_HCHISPACA]  '
						
				--creamos relaciones
				EXEC sp_executesql @SqltablaExamenFisico

	end

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de administración técnica de la historia clínica que crea dinámicamente tablas particionadas por año y mes para almacenar los valores registrados en formularios clínicos. Según el parámetro de mes y año recibido, genera (si aún no existen) cuatro tablas con estructura heredada de sus plantillas base: OTVALORES (otros valores/campos adicionales de la historia), RSVALORES (revisión por sistemas), ANTVALORES (antecedentes del paciente) y EXAVALORES (examen físico). Cada tabla creada queda vinculada mediante llaves foráneas a la historia clínica del paciente (HCHISPACA), a los grupos de variables y a los catálogos de variables y listas de opciones correspondientes a cada sección. Su propósito es particionar el volumen de registros clínicos por período para mejorar el rendimiento, siendo invocado típicamente al inicio de cada mes como proceso de mantenimiento de la estructura de datos de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HCCrearTablaValoresDinamicos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HCCrearTablaValoresDinamicos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Crea dinámicamente, por período año-mes, las tablas particionadas de valores clínicos (Otros, Revisión por Sistemas, Antecedentes y Examen Físico) clonando la estructura de las tablas base y aplicando PK y FKs hacia los catálogos correspondientes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HCCrearTablaValoresDinamicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir las tablas base OTVALORES, RSVALORES, ANTVALORES y EXAVALORES como plantilla de estructura.; Deben existir las tablas referenciadas por las FKs: HCHISPACA, OTGRUPO, OTVARIABLES, OTVARIABLESL, RSGRUPO, RSVARIABLES, RSVARIABLESL, ANTVARIABLES, ANTVARIABLESL, EXAGRUPO, EXAVARIABLES, EXAVARIABLESL.; El sufijo de período (año-mes) debe ser válido para nombrar objetos SQL.; El usuario ejecutor debe tener permisos para crear tablas y constraints en el esquema dbo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HCCrearTablaValoresDinamicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La operación es idempotente: nunca recrea ni altera una tabla de período que ya exista.; Cada tabla creada por período tiene PK CLUSTERED sobre la columna ID.; Las tablas de período mantienen integridad referencial hacia el cabezal de historia clínica HCHISPACA.; Las nuevas tablas se crean vacías (SELECT TOP 0) preservando solo la estructura de la tabla base.; El nombre de cada tabla derivada se forma como prefijo fijo (OT/RS/ANT/EXA)VALORES + sufijo de período.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HCCrearTablaValoresDinamicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica; Revisión por sistemas; Antecedentes; Examen físico; Variables clínicas dinámicas; Particionamiento por período año-mes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HCCrearTablaValoresDinamicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.OTVALORES{AnoMes}: Si no existe una tabla de usuario con nombre OTVALORES+AnoMes, se crea vía SELECT TOP 0 * INTO desde OTVALORES y se le agregan PK sobre ID y FKs hacia HCHISPACA, OTGRUPO, OTVARIABLES y OTVARIABLESL.; [INSERT] dbo.RSVALORES{AnoMes}: Si no existe la tabla RSVALORES+AnoMes, se crea desde RSVALORES con SELECT TOP 0 * INTO y se le agregan PK sobre ID y FKs hacia HCHISPACA, RSGRUPO, RSVARIABLES y RSVARIABLESL.; [INSERT] dbo.ANTVALORES{AnoMes}: Si no existe la tabla ANTVALORES+AnoMes, se crea desde ANTVALORES con SELECT TOP 0 * INTO y se le agregan PK sobre ID y FKs hacia ANTVARIABLES, ANTVARIABLESL y HCHISPACA (incluye una FK duplicada sobre IDANTVARIABLE nombrada FK_..._RSVARIABLES que también referencia ANTVARIABLES).; [INSERT] dbo.EXAVALORES{AnoMes}: Si no existe la tabla EXAVALORES+AnoMes, se crea desde EXAVALORES con SELECT TOP 0 * INTO y se le agregan PK sobre ID y FKs hacia EXAGRUPO, EXAVARIABLES, EXAVARIABLESL y HCHISPACA.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HCCrearTablaValoresDinamicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe en sysobjects una tabla de usuario llamada OTVALORES+AnoMes → Crea la tabla particionada OTVALORES del período y aplica sus constraints else No hace nada para Otros; si No existe en sysobjects una tabla de usuario llamada RSVALORES+AnoMes → Crea la tabla particionada RSVALORES del período y aplica sus constraints else No hace nada para Revisión por Sistemas; si No existe en sysobjects una tabla de usuario llamada ANTVALORES+AnoMes → Crea la tabla particionada ANTVALORES del período y aplica sus constraints else No hace nada para Antecedentes; si No existe en sysobjects una tabla de usuario llamada EXAVALORES+AnoMes → Crea la tabla particionada EXAVALORES del período y aplica sus constraints else No hace nada para Examen Físico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HCCrearTablaValoresDinamicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'sys.sysobjects; dbo.OTVALORES; dbo.RSVALORES; dbo.ANTVALORES; dbo.EXAVALORES', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HCCrearTablaValoresDinamicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HCCrearTablaValoresDinamicos';
-- GO
