CREATE TABLE [Payroll].[ContributorTypeSubtype] (
    [Id]                   INT          IDENTITY (1, 1) NOT NULL,
    [ContributorTypeId]    INT          NOT NULL,
    [ContributorSubtypeId] INT          NOT NULL,
    [State]                BIT          NOT NULL CONSTRAINT [DF_CTS_State] DEFAULT ((1)),
    [CreationUser]         VARCHAR (100) NOT NULL,
    [CreationDate]         DATETIME     NOT NULL CONSTRAINT [DF_CTS_CreationDate] DEFAULT (GETDATE()),
    [ModificationUser]     VARCHAR (100) NULL,
    [ModificationDate]     DATETIME     NULL,
    [TimeStamp]            ROWVERSION   NOT NULL,
    CONSTRAINT [PK_ContributorTypeSubtype]  PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CTS_ContributorType]     FOREIGN KEY ([ContributorTypeId])    REFERENCES [Payroll].[ContributorType] ([Id]),
    CONSTRAINT [FK_CTS_ContributorSubtype]  FOREIGN KEY ([ContributorSubtypeId]) REFERENCES [Payroll].[ContributorSubtype] ([Id]),
    CONSTRAINT [UQ_CTS_Type_Subtype]        UNIQUE NONCLUSTERED ([ContributorTypeId] ASC, [ContributorSubtypeId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal ROWVERSION para control de concurrencia optimista.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContributorTypeSubtype', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación. NULL si nunca fue modificado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContributorTypeSubtype', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación. NULL si nunca fue modificado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContributorTypeSubtype', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación. DEFAULT GETDATE().', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContributorTypeSubtype', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro. VARCHAR(100).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContributorTypeSubtype', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo (BIT: 1=Activo, 0=Inactivo). DEFAULT 1.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContributorTypeSubtype', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK al subtipo de cotizante. INT NOT NULL. Referencia [Payroll].[ContributorSubtype]([Id]).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContributorTypeSubtype', @level2type = N'COLUMN', @level2name = N'ContributorSubtypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK al tipo de cotizante. INT NOT NULL. Referencia [Payroll].[ContributorType]([Id]).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContributorTypeSubtype', @level2type = N'COLUMN', @level2name = N'ContributorTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único. INT IDENTITY, clave primaria.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContributorTypeSubtype', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de relación entre tipos y subtipos de cotizante válidos para PILA. Permite restringir qué combinaciones tipo/subtipo son admisibles para un empleado, simplificando la selección en la generación del archivo plano y reduciendo errores de validación en PILA. FK referenciado desde [Payroll].[Employee].[ContributorTypeSubtypeId].', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContributorTypeSubtype';
