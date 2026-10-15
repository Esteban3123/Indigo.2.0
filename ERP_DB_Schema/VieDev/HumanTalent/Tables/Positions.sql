CREATE TABLE [HumanTalent].[Positions] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)  NOT NULL,
    [Name]                 VARCHAR (100) NOT NULL,
    [HeadId]               INT           NULL,
    [PositionId]           INT           NULL,
    [OrganizationalUnitId] INT           NOT NULL,
    [CostCenterId]         INT           NOT NULL,
    [ManagementId]         INT           NOT NULL,
    [BranchOfficeId]       INT           NOT NULL,
    [Amount]               INT           NOT NULL,
    [DuracionProcess]      INT           NULL,
    [frmPositionId]        INT           NOT NULL,
    [aprovationDate]       DATETIME      NOT NULL,
    [UrlSupport]           VARCHAR (50)  NULL,
    [State]                BIT           NOT NULL,
    [CreationUser]         VARCHAR (20)  NULL,
    [CreationDate]         DATETIME      NULL,
    [ModificationUser]     VARCHAR (20)  NULL,
    [ModificationDate]     DATETIME      NULL,
    CONSTRAINT [PK__Position__3214EC073E741DE5] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Positions', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Positions', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Positions', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Positions', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado: False -  Inactivo, True - Activo o 1 - Activo, 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Positions', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Soporte de URL', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Positions', @level2type = N'COLUMN', @level2name = N'UrlSupport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'fecha de aprobación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Positions', @level2type = N'COLUMN', @level2name = N'aprovationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion de la posicion del formulario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Positions', @level2type = N'COLUMN', @level2name = N'frmPositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duracion del proceso', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Positions', @level2type = N'COLUMN', @level2name = N'DuracionProcess';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Positions', @level2type = N'COLUMN', @level2name = N'Amount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  de la sucursal', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Positions', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  de la  gestión', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Positions', @level2type = N'COLUMN', @level2name = N'ManagementId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  del centro de costos', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Positions', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  de la  unidad organizativa', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Positions', @level2type = N'COLUMN', @level2name = N'OrganizationalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  de posición', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Positions', @level2type = N'COLUMN', @level2name = N'PositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  de la cabecera', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Positions', @level2type = N'COLUMN', @level2name = N'HeadId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Positions', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Positions', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Positions', @level2type = N'COLUMN', @level2name = N'Id';

