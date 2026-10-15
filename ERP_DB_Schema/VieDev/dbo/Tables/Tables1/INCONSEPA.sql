CREATE TABLE [dbo].[INCONSEPA] (
    [IDPOBLACI] CHAR (1)     NOT NULL,
    [IDDOCUMEN] VARCHAR (25) NOT NULL,
    [CONSECUTI] INT          NOT NULL,
    [ID]        INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK_INCONSEPA] PRIMARY KEY CLUSTERED ([IDPOBLACI] ASC, [IDDOCUMEN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la trazibilidad de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSEPA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Documento:    -> Para las poblaciones del 0 al 4, es el codigo del Departamento + Codigo del Municipio (tomados del centro de atencion seleccionado)    -> Para la poblacion 5, es la identificacion de la madre o la identificacion de la cabeza de hogar  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSEPA', @level2type = N'COLUMN', @level2name = N'IDDOCUMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de Poblacion Especial:  0. Personas de la tercera edad en protección de ancianatos.  1. Comunidad Indígena que no este identificada por la Registraduría Nacional del Estado Civil   2. Población indigente  3. Población infantil a cargo del ICBF  4. Comunidad indígena menor de edad no identificada por la RNEC  5. Menor de edad recién nacido vivo sin identificar al infante con edad menor o igual a 30 días', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSEPA', @level2type = N'COLUMN', @level2name = N'IDPOBLACI';

