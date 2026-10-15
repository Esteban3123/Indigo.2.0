CREATE TABLE [HumanTalent].[EndowmentsRecords] (
    [Id]                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]        INT           NOT NULL,
    [Action]            TINYINT       NOT NULL,
    [DeliveryDate]      DATETIME      NOT NULL,
    [Observation]       VARCHAR (300) NOT NULL,
    [HiringDocumentsId] INT           NOT NULL,
    [EndowmentsTypeId]  INT           NOT NULL,
    [GarmentsType]      TINYINT       NOT NULL,
    [EstimatedDate]     DATETIME      NOT NULL,
    [Antiquity]         INT           NOT NULL,
    [CreationUser]      VARCHAR (50)  NOT NULL,
    [CreationDate]      DATETIME      NOT NULL,
    [ModificationUser]  VARCHAR (50)  NULL,
    [ModificationDate]  DATETIME      NULL,
    [ReturnDate]        DATETIME      NULL,
    CONSTRAINT [PK_EndowmentsRecords] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EndowmentsRecords_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_EndowmentsRecords_EndowmentsType] FOREIGN KEY ([EndowmentsTypeId]) REFERENCES [HumanTalent].[EndowmentsType] ([Id]),
    CONSTRAINT [FK_EndowmentsRecords_HiringDocuments] FOREIGN KEY ([HiringDocumentsId]) REFERENCES [HumanTalent].[HiringDocuments] ([Id])
);


GO
ALTER TABLE [HumanTalent].[EndowmentsRecords] NOCHECK CONSTRAINT [FK_EndowmentsRecords_Employee];


GO
ALTER TABLE [HumanTalent].[EndowmentsRecords] NOCHECK CONSTRAINT [FK_EndowmentsRecords_EndowmentsType];


GO
ALTER TABLE [HumanTalent].[EndowmentsRecords] NOCHECK CONSTRAINT [FK_EndowmentsRecords_HiringDocuments];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de regreso', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsRecords', @level2type = N'COLUMN', @level2name = N'ReturnDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsRecords', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ModificaciónUsuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsRecords', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsRecords', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CreaciónUsuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsRecords', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antigüedad', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsRecords', @level2type = N'COLUMN', @level2name = N'Antiquity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha estimada de entrega según el parámetro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsRecords', @level2type = N'COLUMN', @level2name = N'EstimatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de entrega: 1-Vestuario, 2-Calzado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsRecords', @level2type = N'COLUMN', @level2name = N'GarmentsType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DotacionesTypeId', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsRecords', @level2type = N'COLUMN', @level2name = N'EndowmentsTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ContrataciónDocumentosId', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsRecords', @level2type = N'COLUMN', @level2name = N'HiringDocumentsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsRecords', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de entrega', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsRecords', @level2type = N'COLUMN', @level2name = N'DeliveryDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo de accion: 1-Entrega, 2-Devolución', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsRecords', @level2type = N'COLUMN', @level2name = N'Action';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsRecords', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsRecords', @level2type = N'COLUMN', @level2name = N'Id';

