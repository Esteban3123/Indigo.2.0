CREATE TABLE [dbo].[HCREGSIGN] (
    [IDREGANES]   INT       NOT NULL,
    [HORAREGIS]   CHAR (5)  NOT NULL,
    [VALORSIGN]   CHAR (20) NOT NULL,
    [OPCIOSIGN]   CHAR (10) NOT NULL,
    [SURGERYDATE] DATETIME  NULL,
    CONSTRAINT [PK_HCREGSIGN_1] PRIMARY KEY CLUSTERED ([IDREGANES] ASC, [HORAREGIS] ASC, [OPCIOSIGN] ASC, [VALORSIGN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha completa de la Cirugia para listar de manera correcta el formulario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGSIGN', @level2type = N'COLUMN', @level2name = N'SURGERYDATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de l Convecion estipulada entre el rango (210-10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGSIGN', @level2type = N'COLUMN', @level2name = N'OPCIOSIGN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del signo Entre el rango 210- 10', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGSIGN', @level2type = N'COLUMN', @level2name = N'VALORSIGN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Registro Signos Vitales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGSIGN', @level2type = N'COLUMN', @level2name = N'HORAREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Registrado en la Cabecera de Anestesia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGSIGN', @level2type = N'COLUMN', @level2name = N'IDREGANES';

