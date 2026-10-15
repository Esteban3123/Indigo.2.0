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
ALTER TABLE [Contract].[CupsSubgroup] NOCHECK CONSTRAINT [FK_CupsSubgroup_CupsGroup];


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de grupo de imagenología, tabla RISGRIMAGE', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'IdRisGrImage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mostrar en dashboard imagenologia, este campo se activa cuando el grupo seleccionado es de tipo imagenologia, este campo se agrego para ser utilizado por el equipo de Crystal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'ShowOnImagingDashboard';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del manual tarifario  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del subgrupo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del subgrupo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de subgrupo de CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo del CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'CupsGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del subgrupo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'Id';

