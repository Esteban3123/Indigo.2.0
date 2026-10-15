CREATE TABLE [dbo].[CHCTRLCOM] (
    [CODTIPDIE] CHAR (1)     NOT NULL,
    [HORINICOM] DATETIME     NOT NULL,
    [HORFINCOM] DATETIME     NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_CHCTRLCOM] PRIMARY KEY CLUSTERED ([CODTIPDIE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCTRLCOM', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora maxima para solicitar alimentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCTRLCOM', @level2type = N'COLUMN', @level2name = N'HORFINCOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora en la que se puede iniciar la solicitud de alimentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCTRLCOM', @level2type = N'COLUMN', @level2name = N'HORINICOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de  Dieta:  1: Desayuno  2: Almuerzo  3: Cena  4: Complemento mañana  5: Complemento tarde  6: Otro  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCTRLCOM', @level2type = N'COLUMN', @level2name = N'CODTIPDIE';

