CREATE TABLE [Admissions].[ElectronicSignature] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [CODCENATE]           CHAR (10)     NOT NULL,
    [ElectronicInterface] BIT           NOT NULL,
    [URL]                 VARCHAR (250) NOT NULL,
    [APIToken]            VARCHAR (100) NOT NULL,
    [Supplier]            VARCHAR (100) NOT NULL,
    [ValidityTime]        INT           NOT NULL,
    [UserCreation]        CHAR (20)     NOT NULL,
    [DateCreation]        DATETIME      NOT NULL,
    [UserModification]    CHAR (20)     NULL,
    [DateModification]    DATETIME      NULL,
    CONSTRAINT [PK_ElectronicSignature] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicSignature_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la fecha de modificación', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'DateModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el usuario quien lo modifico', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'UserModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la fecha de creación  de las firmas electrónicas', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'DateCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el usuario de creación', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'UserCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'para llever el tiempo maximo de vigencia para firmantes', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'ValidityTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'para usuario o proveedor firma electronica    1=Zapsign     2 =By Truora', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'Supplier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'guarda las respectivas API Token', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'APIToken';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Para introducir la URL de la interfaz', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'URL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica si realiza Interfaz', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'ElectronicInterface';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del centro de atención', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'Id';

