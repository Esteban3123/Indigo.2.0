CREATE TABLE [SelfService].[PortalUserCompany] (
    [Id]                     INT           IDENTITY (1, 1) NOT NULL,
    [PortalUserId]           INT           NOT NULL,
    [CompanyName]            VARCHAR (100) NOT NULL,
    [TransactionalContainer] VARCHAR (30)  NOT NULL,
    [EmployeeId]             INT           NOT NULL,
    CONSTRAINT [PK_PortalUserCompany] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortalUserCompany_PortalUser] FOREIGN KEY ([PortalUserId]) REFERENCES [SelfService].[PortalUser] ([Id])
);




GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre los usuarios del portal de autoservicio y las empresas o compañías a las que pertenecen, permitiendo que un mismo usuario acceda a múltiples entidades o contenedores transaccionales.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUserCompany';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUserCompany';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único del registro de vinculación usuario-empresa.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUserCompany', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUserCompany', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario registrado en el portal de autoservicio.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUserCompany', @level2type = N'COLUMN', @level2name = N'PortalUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUserCompany', @level2type = N'COLUMN', @level2name = N'PortalUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la empresa o compañía a la que está asociado el usuario del portal.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUserCompany', @level2type = N'COLUMN', @level2name = N'CompanyName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUserCompany', @level2type = N'COLUMN', @level2name = N'CompanyName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenedor o base de datos transaccional correspondiente a la empresa, usado para enrutar las operaciones del usuario a la entidad correcta.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUserCompany', @level2type = N'COLUMN', @level2name = N'TransactionalContainer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUserCompany', @level2type = N'COLUMN', @level2name = N'TransactionalContainer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del empleado dentro de la empresa, vincula al usuario del portal con su registro de empleado.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUserCompany', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'PortalUserCompany', @level2type = N'COLUMN', @level2name = N'EmployeeId';
