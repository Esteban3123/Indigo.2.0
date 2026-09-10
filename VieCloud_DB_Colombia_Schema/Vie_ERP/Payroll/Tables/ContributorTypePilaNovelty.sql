CREATE TABLE [Payroll].[ContributorTypePilaNovelty] (
    [Id]                INT          IDENTITY (1, 1) NOT NULL,
    [ContributorTypeId] INT          NOT NULL,
    [PilaNoveltyTypeId] INT          NOT NULL,
    [State]             BIT          NOT NULL CONSTRAINT [DF_CTPN_State] DEFAULT ((1)),
    [CreationUser]      VARCHAR (100) NOT NULL,
    [CreationDate]      DATETIME     NOT NULL CONSTRAINT [DF_CTPN_CreationDate] DEFAULT (GETDATE()),
    [ModificationUser]  VARCHAR (100) NULL,
    [ModificationDate]  DATETIME     NULL,
    [TimeStamp]         ROWVERSION   NOT NULL,
    CONSTRAINT [PK_ContributorTypePilaNovelty] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CTPN_ContributorType] FOREIGN KEY ([ContributorTypeId]) REFERENCES [Payroll].[ContributorType] ([Id]),
    CONSTRAINT [FK_CTPN_PilaNoveltyType] FOREIGN KEY ([PilaNoveltyTypeId]) REFERENCES [Payroll].[PilaNoveltyType] ([Id]),
    CONSTRAINT [UQ_CTPN_Type_Novelty]    UNIQUE NONCLUSTERED ([ContributorTypeId] ASC, [PilaNoveltyTypeId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal ROWVERSION para control de concurrencia optimista.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContributorTypePilaNovelty', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación. NULL si nunca fue modificado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContributorTypePilaNovelty', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación. NULL si nunca fue modificado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContributorTypePilaNovelty', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación. DEFAULT GETDATE().', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContributorTypePilaNovelty', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro. VARCHAR(100).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContributorTypePilaNovelty', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo (BIT: 1=Activo, 0=Inactivo). DEFAULT 1.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContributorTypePilaNovelty', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK al tipo de novedad PILA permitido. INT NOT NULL. Referencia [Payroll].[PilaNoveltyType]([Id]).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContributorTypePilaNovelty', @level2type = N'COLUMN', @level2name = N'PilaNoveltyTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK al tipo de cotizante. INT NOT NULL. Referencia [Payroll].[ContributorType]([Id]).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContributorTypePilaNovelty', @level2type = N'COLUMN', @level2name = N'ContributorTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único. INT IDENTITY, clave primaria.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContributorTypePilaNovelty', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre tipos de cotizante y los tipos de novedad PILA que pueden reportar. Permite parametrizar qué novedades (IGE, LMA, VAC, SLN, etc.) son válidas para cada tipo de cotizante en la generación del archivo plano PILA.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContributorTypePilaNovelty';
