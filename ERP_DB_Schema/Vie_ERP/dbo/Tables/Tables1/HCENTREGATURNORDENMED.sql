CREATE TABLE [dbo].[HCENTREGATURNORDENMED] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [IDHCTURNO]    INT            NOT NULL,
    [IPCODPACI]    VARCHAR (25)   NULL,
    [CODSERIPS]    CHAR (25)      NULL,
    [NUMINGRES]    CHAR (25)      NULL,
    [ORDENMED]     CHAR (25)      NULL,
    [DESESPECI]    CHAR (50)      NULL,
    [FECORDMED]    DATETIME       NULL,
    [FECHASUGE]    DATETIME       NULL,
    [DESSERIPS]    CHAR (250)     NULL,
    [Name]         CHAR (250)     NULL,
    [NOMMEDICO]    CHAR (150)     NULL,
    [PRISERIPS]    CHAR (250)     NULL,
    [SOLICITASALA] CHAR (10)      NULL,
    [EXREASITI]    CHAR (10)      NULL,
    [ESTSERIPS]    CHAR (25)      NULL,
    [CANSERIPS]    CHAR (250)     NULL,
    [OBSSERIPS]    VARCHAR (2000) NULL,
    [UFUDESCRI]    CHAR (250)     NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda descripción de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNORDENMED', @level2type = N'COLUMN', @level2name = N'UFUDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Observacion Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNORDENMED', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNORDENMED', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNORDENMED', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica si el examen es realizado en sitio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNORDENMED', @level2type = N'COLUMN', @level2name = N'EXREASITI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica si este servicio solicitado exige sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNORDENMED', @level2type = N'COLUMN', @level2name = N'SOLICITASALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Prioridad del Servicio Solicitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNORDENMED', @level2type = N'COLUMN', @level2name = N'PRISERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Nombre del medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNORDENMED', @level2type = N'COLUMN', @level2name = N'NOMMEDICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Nombre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNORDENMED', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gurad la Descripcion del Procedimiento o Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNORDENMED', @level2type = N'COLUMN', @level2name = N'DESSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Campo que guarda la fecha sugerida para la toma de la imagen DX.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNORDENMED', @level2type = N'COLUMN', @level2name = N'FECHASUGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Fecha de la Orden Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNORDENMED', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda  la descripción de la especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNORDENMED', @level2type = N'COLUMN', @level2name = N'DESESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Orden medica ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNORDENMED', @level2type = N'COLUMN', @level2name = N'ORDENMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Numero ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNORDENMED', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Código Unico de Procedimientos y  Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNORDENMED', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Código del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNORDENMED', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda código o numero de turno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNORDENMED', @level2type = N'COLUMN', @level2name = N'IDHCTURNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNORDENMED', @level2type = N'COLUMN', @level2name = N'ID';

