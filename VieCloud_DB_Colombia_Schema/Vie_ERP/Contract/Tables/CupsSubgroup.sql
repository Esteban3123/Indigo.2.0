CREATE TABLE [Contract].[CupsSubgroup] (
    [Id]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CupsGroupId]            INT           NOT NULL,
    [Code]                   VARCHAR (20)  NOT NULL,
    [Name]                   VARCHAR (300) NOT NULL,
    [Description]            VARCHAR (300) NULL,
    [Status]                 BIT           NOT NULL,
    [CreationUser]           VARCHAR (20)  NOT NULL,
    [CreationDate]           DATETIME      NOT NULL,
    [ModificationUser]       VARCHAR (20)  NULL,
    [ModificationDate]       DATETIME      NULL,
    [TimeStamp]              ROWVERSION    NOT NULL,
    [ShowOnImagingDashboard] BIT           NULL,
    [IdRisGrImage]           INT           NULL,
    CONSTRAINT [PK_CupsSubgroup__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CupsSubgroup_CupsGroup] FOREIGN KEY ([CupsGroupId]) REFERENCES [Contract].[CupsGroup] ([Id])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CupsSubgroup__Code]
    ON [Contract].[CupsSubgroup]([Code] ASC);


GO
CREATE trigger [Contract].[CreateCupsSubGroupInHIS]
on [Contract].[CupsSubgroup]
for INSERT, UPDATE as

declare @Code AS varchar(20)  = (SELECT Code FROM INSERTED) 
declare @Name AS varchar(200)  = (SELECT Name FROM INSERTED) 
declare @Status AS bit  = (SELECT Status FROM INSERTED) 
declare @ShowOnImagingDashboard AS bit  = (SELECT ShowOnImagingDashboard FROM INSERTED) 
declare @CupsGroupId as int = (SELECT CupsGroupId FROM INSERTED) 
DECLARE @IdRisGrImage  as int = (SELECT IdRisGrImage FROM INSERTED)
declare @CodeCupsGroup varchar(20) = (select Code from Contract.CupsGroup where Id = @CupsGroupId)
declare @Existe AS INT = (SELECT count(*) FROM .INCUPSSUB where CODSUBIPS = @Code)

--Se valida si existe el grupo, si no existe se crea
if(SELECT count(*) FROM .INCUPSGRU where CODGRUIPS = @CodeCupsGroup) = 0
begin
	declare @NameCupsGroup varchar(100) = (select Name from Contract.CupsGroup where Id = @CupsGroupId)
	declare @ImagingType bit = (select ImagingType from Contract.CupsGroup where Id = @CupsGroupId)
	insert into .INCUPSGRU(CODGRUIPS, DESGRUIPS, GRUPOIMAG) values(@CodeCupsGroup, @NameCupsGroup, @ImagingType)
end

begin
	if @Existe = 0
	begin
		insert into .INCUPSSUB(CODGRUSUB, CODGRUIPS, CODSUBIPS, DESSUBIPS, MDASHIMAG, IDRISGRIMAGE) values(@CodeCupsGroup + '-' + @Code, @CodeCupsGroup, @Code, @Name, ISNULL(@ShowOnImagingDashboard, 0), @IdRisGrImage)
	end 
	else begin
		update .INCUPSSUB set CODGRUSUB = @CodeCupsGroup + '-' + @Code, DESSUBIPS = @Name, MDASHIMAG = ISNULL(@ShowOnImagingDashboard, 0), IDRISGRIMAGE = @IdRisGrImage where CODSUBIPS = @Code
	end
end
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, nullable) del grupo de imagenología en tabla RISGRIMAGE; vincula el subgrupo con configuraciones específicas de sistemas RIS/PACS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'IdRisGrImage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de grupo de imagenología, tabla RISGRIMAGE', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'IdRisGrImage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'IdRisGrImage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera de visualización (BIT) en dashboard de imagenología; se activa cuando el subgrupo es de tipo imagenología, para mostrar en reportes Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'ShowOnImagingDashboard';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mostrar en dashboard imagenologia, este campo se activa cuando el grupo seleccionado es de tipo imagenologia, este campo se agrego para ser utilizado por el equipo de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'ShowOnImagingDashboard';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'ShowOnImagingDashboard';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) de auditoría; registro automático del instante de creación, modificación o evento del subgrupo para control de versiones.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de última modificación (DATETIME); instante del último cambio aplicado al subgrupo CUPS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de modificación (VARCHAR 20); identificación del último usuario que alteró el registro del subgrupo CUPS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación (DATETIME); instante en que se registró por primera vez el subgrupo CUPS en la base de datos.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación (VARCHAR 20); identificación del usuario que originó el registro del subgrupo CUPS en el sistema.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del subgrupo CUPS (BIT): 1=Activo, 0=Inactivo; indica si el subgrupo está disponible para uso en contratos, facturación y atención.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del manual tarifario  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada (VARCHAR 300) del subgrupo CUPS; información complementaria sobre características, alcance o notas del subgrupo de procedimientos.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del subgrupo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo (VARCHAR 300) del subgrupo CUPS; denominación del conjunto de procedimientos o servicios agrupados en este subgrupo.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del subgrupo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico (VARCHAR 20) del subgrupo CUPS; identificador estándar de procedimiento, servicio o producto sanitario según nomenclatura CUPS nacional.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de subgrupo de CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del grupo CUPS padre; referencia a [Contract].[CupsGroup] que agrupa subgrupos relacionados por clasificación de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'CupsGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo del CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'CupsGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'CupsGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del subgrupo CUPS; clave primaria que identifica unívocamente cada registro de subgrupo en el catálogo de procedimientos y servicios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del subgrupo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subgrupos de servicios CUPS usados en contratos. Permite organizar los procedimientos y servicios de salud en categorías más específicas dentro de un grupo CUPS, facilitando la tarifación, negociación contractual y clasificación para reportes e imágenes diagnósticas.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup';
