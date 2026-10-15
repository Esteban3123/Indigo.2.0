CREATE TABLE [HumanTalent].[ProfessionalExperience] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CandidateId]      INT           NULL,
    [Business]         VARCHAR (50)  NOT NULL,
    [FrmPositionId]    INT           NOT NULL,
    [DateStart]        DATE          NOT NULL,
    [Linked]           INT           NULL,
    [DateEnd]          DATE          NULL,
    [Sector]           INT           NOT NULL,
    [CityId]           INT           NOT NULL,
    [Phone]            VARCHAR (20)  NOT NULL,
    [Area]             INT           NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [Description]      VARCHAR (100) NULL,
    [EmployeeId]       INT           NULL,
    CONSTRAINT [PK__Professi__3214EC0799A38501] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__Professio__FrmPo__1C4CD42B] FOREIGN KEY ([FrmPositionId]) REFERENCES [Payroll].[Position] ([Id]),
    CONSTRAINT [fk_CityDocumentPE] FOREIGN KEY ([CityId]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_ProfessionalExperience_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id])
);


GO
ALTER TABLE [HumanTalent].[ProfessionalExperience] NOCHECK CONSTRAINT [FK__Professio__FrmPo__1C4CD42B];


GO
ALTER TABLE [HumanTalent].[ProfessionalExperience] NOCHECK CONSTRAINT [fk_CityDocumentPE];


GO
ALTER TABLE [HumanTalent].[ProfessionalExperience] NOCHECK CONSTRAINT [FK_ProfessionalExperience_Employee];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  del  empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ProfessionalExperience', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ProfessionalExperience', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ProfessionalExperience', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ProfessionalExperience', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ProfessionalExperience', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ProfessionalExperience', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Área', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ProfessionalExperience', @level2type = N'COLUMN', @level2name = N'Area';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ProfessionalExperience', @level2type = N'COLUMN', @level2name = N'Phone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  de la  ciudad', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ProfessionalExperience', @level2type = N'COLUMN', @level2name = N'CityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sector', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ProfessionalExperience', @level2type = N'COLUMN', @level2name = N'Sector';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de finalización', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ProfessionalExperience', @level2type = N'COLUMN', @level2name = N'DateEnd';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vinculado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ProfessionalExperience', @level2type = N'COLUMN', @level2name = N'Linked';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ProfessionalExperience', @level2type = N'COLUMN', @level2name = N'DateStart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion de la posicion del formulario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ProfessionalExperience', @level2type = N'COLUMN', @level2name = N'FrmPositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Negocio', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ProfessionalExperience', @level2type = N'COLUMN', @level2name = N'Business';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ProfessionalExperience', @level2type = N'COLUMN', @level2name = N'CandidateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'ProfessionalExperience', @level2type = N'COLUMN', @level2name = N'Id';

