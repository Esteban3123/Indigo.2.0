CREATE TABLE [dbo].[HCENCPACD] (
    [AUTO]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AUTOHCENCPACC] INT           NOT NULL,
    [DESPREENC]     VARCHAR (500) NULL,
    [RESPREPAC]     VARCHAR (500) NULL,
    CONSTRAINT [PK_HCENCPACD] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la respuesta de la pregunta realizada al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENCPACD', @level2type = N'COLUMN', @level2name = N'RESPREPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción de la pregunta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENCPACD', @level2type = N'COLUMN', @level2name = N'DESPREENC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico Cabecera ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENCPACD', @level2type = N'COLUMN', @level2name = N'AUTOHCENCPACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENCPACD', @level2type = N'COLUMN', @level2name = N'AUTO';

