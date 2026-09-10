CREATE TABLE [Contract].[CupsHomologation] (
    [Id]           INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CupsEntityId] INT NOT NULL,
    [IPSServiceId] INT NOT NULL,
    CONSTRAINT [PK_CupsHomologation__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CupsHomologation_CupsEntity] FOREIGN KEY ([CupsEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_CupsHomologation_IPSService] FOREIGN KEY ([IPSServiceId]) REFERENCES [Contract].[IPSService] ([Id])
);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CupsHomologation__CupsEntityId__IPSServiceId]
    ON [Contract].[CupsHomologation]([CupsEntityId] ASC, [IPSServiceId] ASC);


GO

-- =============================================
-- Author:		Diego Roldan
-- Create date: 2018-01-16
-- Description:	Trigger para tener actualizados los CUPS en la base de datos de crystal
-- =============================================
CREATE TRIGGER [Contract].[TriggerCUPSHomologationVieCrystal]
   ON  [Contract].[CupsHomologation]
   AFTER INSERT, UPDATE
AS 
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @CupsEntityId int, 
		@IPSServiceId int, 
		@CupsEntityCode varchar(20),
		@Presentation tinyint,
		@MinimunAge int,
		@MinimunAgeUnit tinyint,
		@Description varchar(300),
		@ServiceType tinyint,
		@Status bit

	SELECT @CupsEntityId = I.CupsEntityId, 
		@IPSServiceId = I.IPSServiceId,
		@Presentation = IPS.Presentation,
		@MinimunAge = IPS.MinimunAge,
		@MinimunAgeUnit = IPS.MinimunAgeUnit,
		@CupsEntityCode = CE.Code,
		@ServiceType = CE.ServiceType,
		@Status = CE.[Status],
		@Description = CE.Description
	FROM inserted I
	INNER JOIN [Contract].[CUPSEntity] CE WITH(NOLOCK) ON CE.Id = I.CupsEntityId
	INNER JOIN [Contract].[IPSService] IPS WITH(NOLOCK) ON IPS.Id = I.IPSServiceId

	IF EXISTS(SELECT * FROM dbo.INCUPSIPS WHERE CODSERIPS = @CupsEntityCode) BEGIN
		UPDATE T1 SET T1.DESSERIPS = @Description,
			T1.[PRESERIPS] = @Presentation,
			T1.[EDADMINI] = @MinimunAge,
			T1.[UNIEDADMI] = CASE @MinimunAgeUnit WHEN 1 THEN 2 WHEN 2 THEN 1 ELSE 0 END,
			T1.[CODGRUIPS] = CASE WHEN T1.[CODGRUIPS] IS NULL THEN (SELECT TOP 1 [CODGRUIPS] FROM dbo.INCUPSGRU) ELSE T1.[CODGRUIPS] END,
			T1.[SIPSESTADO] = @Status
		FROM dbo.INCUPSIPS T1
		WHERE T1.CODSERIPS = @CupsEntityCode
	END
	ELSE BEGIN
		INSERT INTO dbo.INCUPSIPS
           ([CODSERIPS]
           ,[DESSERIPS]
           ,[CODGRUSUB]
           ,[NIVSERIPS]
           ,[TIPSERIPS]
           ,[ARSCODIGO]
		   ,[PRESERIPS]
		   ,[CODGRUIPS]
		   ,[EDADMINI]
		   ,[UNIEDADMI]
		   ,[SIPSESTADO])
		   VALUES (@CupsEntityCode, 
				@Description, 
				(SELECT TOP 1 [CODGRUSUB] FROM dbo.INCUPSSUB), 
				2, 
				@ServiceType, 
				(SELECT TOP 1 [ARSCODIGO] FROM dbo.INAREASER),
				@Presentation,
				(SELECT TOP 1 [CODGRUIPS] FROM dbo.INCUPSGRU),
				@MinimunAge,
				CASE @MinimunAgeUnit WHEN 1 THEN 2 WHEN 2 THEN 1 ELSE 0 END,
				1)
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del servicio IPS en el manual tarifario; referencia a IPSService(Id) que mapea códigos de procedimientos, consultas, exámenes o prestaciones según tarifa contractual', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsHomologation', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del codigo del servicio del manual tarifario', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsHomologation', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsHomologation', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del código CUPS de la entidad; referencia a CUPSEntity(Id) que clasifica procedimientos, diagnósticos, servicios o prestaciones según nomenclatura CUPS nacional', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsHomologation', @level2type = N'COLUMN', @level2name = N'CupsEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del cups de la entidad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsHomologation', @level2type = N'COLUMN', @level2name = N'CupsEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsHomologation', @level2type = N'COLUMN', @level2name = N'CupsEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la homologación; registro que vincula un CUPS de entidad con su servicio IPS equivalente en contrato para conciliación tarifaria', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsHomologation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la homologacion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsHomologation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsHomologation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Homologación entre códigos CUPS de una entidad contratante y los códigos de servicio internos de la IPS. Permite mapear el código de procedimiento o servicio que usa el contrato con el código equivalente manejado por la institución.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsHomologation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsHomologation';
