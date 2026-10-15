CREATE TABLE [MedicalHistory].[ElectronicSignaturesignersDetail] (
    [Id]                           INT          IDENTITY (1, 1) NOT NULL,
    [IdHCDOCUMAD]                  NUMERIC (18) NULL,
    [IdElectronicSignaturesigners] INT          NOT NULL,
    [TokenDocument]                VARCHAR (40) NULL,
    [TokenSigner]                  VARCHAR (40) NULL,
    [Notified]                     BIT          DEFAULT ((0)) NOT NULL,
    [CreateDate]                   DATETIME     DEFAULT ('1900-01-01 00:00:00') NULL,
    CONSTRAINT [PK_ElectronicSignaturesignersDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicSignaturesigners_IdElectronicSignaturesigners] FOREIGN KEY ([IdElectronicSignaturesigners]) REFERENCES [MedicalHistory].[ElectronicSignatureSigners] ([Id]),
    CONSTRAINT [FK_HCDOCUMAD_IdHCDOCUMAD] FOREIGN KEY ([IdHCDOCUMAD]) REFERENCES [dbo].[HCDOCUMAD] ([CONSECUTI])
);


GO
ALTER TABLE [MedicalHistory].[ElectronicSignaturesignersDetail] NOCHECK CONSTRAINT [FK_ElectronicSignaturesigners_IdElectronicSignaturesigners];


GO
ALTER TABLE [MedicalHistory].[ElectronicSignaturesignersDetail] NOCHECK CONSTRAINT [FK_HCDOCUMAD_IdHCDOCUMAD];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Token del firmante del documento', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'TokenSigner';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Token del documento creado en zapsign', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'TokenDocument';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id consecutivo de la tabla de firmantes  ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'IdElectronicSignaturesigners';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutvio de la tabla de documentos.  ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'IdHCDOCUMAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ElectronicSignaturesignersDetail', @level2type = N'COLUMN', @level2name = N'Id';

