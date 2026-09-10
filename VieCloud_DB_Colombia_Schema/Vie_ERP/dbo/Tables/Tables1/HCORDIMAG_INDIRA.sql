CREATE TABLE [dbo].[HCORDIMAG_INDIRA] (
    [TIPO]           CHAR (1)     NULL,
    [ORDEN_VIE]      INT          NULL,
    [INGRESO_VIE]    CHAR (10)    NULL,
    [CUPS]           CHAR (20)    NULL,
    [IDENTIFICACION] VARCHAR (25) NULL,
    [ESTADO]         VARCHAR (25) NULL,
    [FECHA]          DATETIME     NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la imagen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG_INDIRA', @level2type = N'COLUMN', @level2name = N'FECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la imagen (cancelado o con lectura)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG_INDIRA', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG_INDIRA', @level2type = N'COLUMN', @level2name = N'IDENTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Códificacion única de procedimientos en salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG_INDIRA', @level2type = N'COLUMN', @level2name = N'CUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG_INDIRA', @level2type = N'COLUMN', @level2name = N'INGRESO_VIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código auto númerico de la tabla HCORDIMAG', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG_INDIRA', @level2type = N'COLUMN', @level2name = N'ORDEN_VIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Si la imagen es de ambito ambulatorio o intrahospitalario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAG_INDIRA', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla auxiliar que almacena órdenes de imágenes diagnósticas integradas con el sistema INDIRA. Registra por paciente (identificación) y número de ingreso el procedimiento solicitado mediante código CUPS, indicando si el ámbito es ambulatorio o intrahospitalario. El estado refleja si la imagen fue cancelada o cuenta con lectura, junto con la fecha del evento. Referencia registros de la tabla HCORDIMAG mediante su clave autonumérica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'HCORDIMAG_INDIRA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'HCORDIMAG_INDIRA';
GO
