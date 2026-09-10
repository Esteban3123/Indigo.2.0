CREATE TABLE [Payroll].[ConceptClassNoveltyLineType] (
    [Id]                INT          IDENTITY (1, 1) NOT NULL,
    [ConceptClassId]    INT          NOT NULL,
    [NoveltyLineTypeId] INT          NOT NULL,
    [Role]              VARCHAR (50) NOT NULL,
    [State]             BIT          NOT NULL CONSTRAINT [DF_CCNLT_State] DEFAULT ((1)),
    [CreationUser]      VARCHAR (100) NOT NULL,
    [CreationDate]      DATETIME     NOT NULL CONSTRAINT [DF_CCNLT_CreationDate] DEFAULT (GETDATE()),
    [ModificationUser]  VARCHAR (100) NULL,
    [ModificationDate]  DATETIME     NULL,
    [TimeStamp]         ROWVERSION   NOT NULL,
    CONSTRAINT [PK_ConceptClassNoveltyLineType]    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CCNLT_ConceptClass]    FOREIGN KEY ([ConceptClassId])    REFERENCES [Payroll].[ConceptClass] ([Id]),
    CONSTRAINT [FK_CCNLT_NoveltyLineType] FOREIGN KEY ([NoveltyLineTypeId]) REFERENCES [Payroll].[NoveltyLineType] ([Id]),
    CONSTRAINT [UQ_CCNLT_Class_LineType_Role] UNIQUE NONCLUSTERED ([ConceptClassId] ASC, [NoveltyLineTypeId] ASC, [Role] ASC),
    CONSTRAINT [CK_CCNLT_Role] CHECK ([Role] IN (
        'Salary', 'Pension', 'Health', 'OccupationalRisk', 'CompensationFund',
        'Sena', 'Icbf', 'Fsp', 'TransitorySalary', 'ContractDetail',
        'VacationSalary', 'VacationX', 'AvpContribution'
    ))
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal ROWVERSION para control de concurrencia optimista.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptClassNoveltyLineType', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación. NULL si nunca fue modificado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptClassNoveltyLineType', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación. NULL si nunca fue modificado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptClassNoveltyLineType', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación. DEFAULT GETDATE().', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptClassNoveltyLineType', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro. VARCHAR(100).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptClassNoveltyLineType', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo (BIT: 1=Activo, 0=Inactivo). DEFAULT 1.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptClassNoveltyLineType', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rol que cumple la clase de concepto dentro de la línea de novedad PILA. VARCHAR(50). Valores: Salary, Pension, Health, OccupationalRisk, CompensationFund, Sena, Icbf, Fsp, TransitorySalary, ContractDetail, VacationSalary, VacationX, AvpContribution.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptClassNoveltyLineType', @level2type = N'COLUMN', @level2name = N'Role';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK al tipo de línea de novedad PILA. INT NOT NULL. Referencias [Payroll].[NoveltyLineType]([Id]).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptClassNoveltyLineType', @level2type = N'COLUMN', @level2name = N'NoveltyLineTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a la clase de concepto de nómina. INT NOT NULL. Referencia [Payroll].[ConceptClass]([Id]).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptClassNoveltyLineType', @level2type = N'COLUMN', @level2name = N'ConceptClassId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único. INT IDENTITY, clave primaria.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptClassNoveltyLineType', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de relación entre clases de concepto de nómina y tipos de línea de novedad PILA. Define qué rol cumple cada clase de concepto (Salary, Pension, Health, etc.) dentro de cada tipo de línea del archivo plano PILA (Registro Tipo 2).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptClassNoveltyLineType';
