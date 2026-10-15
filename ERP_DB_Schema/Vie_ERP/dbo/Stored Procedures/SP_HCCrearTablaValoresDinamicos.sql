
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