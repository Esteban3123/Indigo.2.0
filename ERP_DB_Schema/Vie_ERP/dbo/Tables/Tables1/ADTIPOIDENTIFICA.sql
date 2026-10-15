CREATE TABLE [dbo].[ADTIPOIDENTIFICA] (
    [ID]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]          INT           NOT NULL,
    [SIGLA]           VARCHAR (2)   NOT NULL,
    [NOMBRE]          VARCHAR (100) NOT NULL,
    [ESTADO]          BIT           NOT NULL,
    [USUARIOCREA]     CHAR (20)     NOT NULL,
    [FECHACREA]       DATETIME      NOT NULL,
    [USUARIOMODIFICA] CHAR (20)     NULL,
    [FECHAMODIFICA]   DATETIME      NULL,
    [MinimunLength]   INT           NULL,
    [MaximumLength]   INT           NULL,
    [TreasuryCode]    VARCHAR (2)   NULL,
    CONSTRAINT [PK_ADTIPOIDENTIFICA] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ADTIPOIDENTIFICA_1]
    ON [dbo].[ADTIPOIDENTIFICA]([ESTADO] ASC, [SIGLA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la longitud maxima ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'MaximumLength';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la longitud minima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'MinimunLength';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la fecha en que fue modificado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el usuario quien modifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Fecha creacion registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'FECHACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Usuario quien creo el registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'USUARIOCREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el estado  1 = Activo  0 = Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1=CC -  Cédula de Ciudadanía  2=CE  -  Cédula de Extranjería  3=TI   -  Tarjeta de Identidad  4=RC  -  Registro Civil  5=PA  -  Pasaporte  6=AS  -  Adulto Sin Identificación  7=MS  -  Menor Sin Identificación  8=NU  -  Número único de identificación personal  9= CN  -  Certificado Nacido Vivo  10= CD  -  Carnet Diplomático (Aplica para extranjeros)  11= SC  -  Salvoconducto (Aplica para extranjeros)  12=PE  -  Permiso especial de Permanencia (Aplica para extranjeros)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1=CC -  Cédula de Ciudadanía  2=CE  -  Cédula de Extranjería  3=TI   -  Tarjeta de Identidad  4=RC  -  Registro Civil  5=PA  -  Pasaporte  6=AS  -  Adulto Sin Identificación  7=MS  -  Menor Sin Identificación  8=NU  -  Número único de identificación personal  9= CN  -  Certificado Nacido Vivo  10= CD  -  Carnet Diplomático (Aplica para extranjeros)  11= SC  -  Salvoconducto (Aplica para extranjeros)  12=PE  -  Permiso especial de Permanencia (Aplica para extranjeros)    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'SIGLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'ID';

