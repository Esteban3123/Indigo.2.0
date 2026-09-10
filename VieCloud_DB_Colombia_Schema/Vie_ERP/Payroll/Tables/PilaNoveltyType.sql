CREATE TABLE [Payroll].[PilaNoveltyType] (
    [Id]               INT          IDENTITY (1, 1) NOT NULL,
    [Code]             VARCHAR (10) NOT NULL,
    [Description]      VARCHAR (200) NOT NULL,
    [PilaField]        TINYINT      NOT NULL,
    [State]            BIT          NOT NULL CONSTRAINT [DF_PilaNoveltyType_State] DEFAULT ((1)),
    [CreationUser]     VARCHAR (100) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL CONSTRAINT [DF_PilaNoveltyType_CreationDate] DEFAULT (GETDATE()),
    [ModificationUser] VARCHAR (100) NULL,
    [ModificationDate] DATETIME     NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    CONSTRAINT [PK_PilaNoveltyType]      PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_PilaNoveltyType_Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal ROWVERSION para control de concurrencia optimista.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PilaNoveltyType', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación. NULL si nunca fue modificado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PilaNoveltyType', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación. NULL si nunca fue modificado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PilaNoveltyType', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación. DEFAULT GETDATE().', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PilaNoveltyType', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro. VARCHAR(100).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PilaNoveltyType', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo (BIT: 1=Activo, 0=Inactivo). DEFAULT 1.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PilaNoveltyType', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de campo PILA (Registro Tipo 2) al que corresponde este tipo de novedad. TINYINT NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PilaNoveltyType', @level2type = N'COLUMN', @level2name = N'PilaField';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del tipo de novedad PILA. VARCHAR(200).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PilaNoveltyType', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del tipo de novedad PILA. VARCHAR(10), clave única.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PilaNoveltyType', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del tipo de novedad PILA. INT IDENTITY, clave primaria.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PilaNoveltyType', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de novedad del archivo PILA (Res. 2388/2016). Cada registro representa un tipo de novedad reportable en el Registro Tipo 2 (IGE, LMA, SLN, VAC, ING, RET, etc.) con su campo correspondiente en el layout.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PilaNoveltyType';
