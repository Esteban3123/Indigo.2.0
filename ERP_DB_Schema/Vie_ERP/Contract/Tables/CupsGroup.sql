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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el grupo es de tipo imagenologia, este campo es utilizado por el equipo de Crystal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'ImagingType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del manual tarifario  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del grupo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del grupo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del grupo del CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo de CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsGroup', @level2type = N'COLUMN', @level2name = N'Id';

