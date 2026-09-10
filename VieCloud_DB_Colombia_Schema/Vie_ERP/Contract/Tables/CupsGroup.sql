CREATE TABLE [Contract].[CupsGroup] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [Description]      VARCHAR (300) NULL,
    [Status]           BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    [ImagingType]      BIT           CONSTRAINT [DF_CupsGroup_ImagingType] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CupsGroup__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CupsGroup__Code]
    ON [Contract].[CupsGroup]([Code] ASC);


GO
CREATE trigger [Contract].[CreateCupsGroupInHIS]
on [Contract].[CupsGroup]
for INSERT, UPDATE as

declare @Code AS varchar(20)  = (SELECT Code FROM INSERTED) 
declare @Name AS varchar(200)  = (SELECT Name FROM INSERTED) 
declare @Status AS bit  = (SELECT Status FROM INSERTED) 
declare @ImagingType AS bit  = (SELECT ImagingType FROM INSERTED) 
declare @Existe AS INT = (SELECT count(*) FROM .INCUPSGRU where CODGRUIPS = @Code)

begin
	if @Existe = 0
	begin
		insert into .INCUPSGRU(CODGRUIPS, DESGRUIPS, GRUPOIMAG) values(@Code, @Name, @ImagingType)
	end 
	else begin
		update .INCUPSGRU set DESGRUIPS = @Name, GRUPOIMAG = @ImagingType where CODGRUIPS = @Code
	end
end
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT, 1=Sí, 0=No) que especifica si el grupo CUPS es de tipo imagenología/radiología. Utilizado por equipo Crystal para clasificación de procedimientos de diagnóstico por imagen.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'ImagingType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el grupo es de tipo imagenologia, este campo es utilizado por el equipo de Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'ImagingType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'ImagingType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática (TIMESTAMP SQL Server) que registra el instante exacto de creación, modificación o cambio de estado del registro del grupo CUPS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del grupo CUPS. Nulo si no ha sido editado desde su creación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario (VARCHAR 20) que realizó la última modificación o actualización del grupo CUPS. Nulo si no ha sido editado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro del grupo CUPS en el contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario (VARCHAR 20) que creó inicialmente el registro del grupo CUPS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del grupo CUPS (BIT): 1=Activo, 0=Inactivo. Determina si el grupo de procedimientos está vigente en el manual tarifario del contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del manual tarifario  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual (VARCHAR 300) del grupo CUPS. Texto opcional que amplía la información sobre el contenido y alcance del grupo de procedimientos.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del grupo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del grupo CUPS (VARCHAR 100). Denominación comercial o técnica del conjunto de procedimientos agrupados.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del grupo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador único del grupo CUPS (VARCHAR 20). Clave normativa que agrupa procedimientos de facturación y RIPS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del grupo del CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico único (INT IDENTITY) del grupo CUPS. Clave primaria para referencia en relaciones de contrato y procedimientos.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupos de códigos CUPS (procedimientos y servicios de salud) usados en contratos. Permite clasificar servicios como imágenes diagnósticas, laboratorios u otros procedimientos para agruparlos y aplicarles condiciones contractuales.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup';
