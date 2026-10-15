CREATE TABLE [dbo].[CHVARIABLES] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [IDGRUPO]       INT            NOT NULL,
    [VARIABLE]      VARCHAR (150)  NOT NULL,
    [TIPO]          TINYINT        NOT NULL,
    [GENERO]        TINYINT        NOT NULL,
    [EDADMIN]       INT            NOT NULL,
    [EDADMAX]       INT            NOT NULL,
    [MANEJADECIMAL] BIT            NULL,
    [CODUSUCRE]     CHAR (20)      NOT NULL,
    [FECHACREA]     DATETIME       NOT NULL,
    [CODUSUMOD]     CHAR (20)      NULL,
    [FECHAMOD]      DATETIME       NULL,
    [TIPOESCALA]    TINYINT        NULL,
    [TOOLTIP]       BIT            CONSTRAINT [DF__CHVARIABL__TOOLT__35F862EC] DEFAULT ((0)) NOT NULL,
    [TOOLTIPTXT]    VARCHAR (1000) NULL,
    CONSTRAINT [PK_CHVARIABLES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CHVARIABLES_CHGRUPO] FOREIGN KEY ([IDGRUPO]) REFERENCES [dbo].[CHGRUPO] ([ID])
);


GO
ALTER TABLE [dbo].[CHVARIABLES] NOCHECK CONSTRAINT [FK_CHVARIABLES_CHGRUPO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo de texto para tooltip', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'TOOLTIPTXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Verificar si tiene tooltip', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'TOOLTIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Escala de la lista de chequeo (mirar el FrmHCVariablesCH escala y clasificación)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHAMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo usuario modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo usuario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si maneja decimal o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'MANEJADECIMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad Máxima en años a la que aplica la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad Minima en años a la que aplica la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Genero a la que aplica la variable  1  - Masculino  2 - Femenino  3  - los dos generos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'GENERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de variable  1 - BOOLEAN - CONTROL CHECK  2 - STRING - CONTROL MEMOEDIT  3 - NUMERICO - NUMERIC  4- LISTA - CONTROL GRIDLOOKUPEDIT  5- FECHA  (dd/MM/yyyy)  6- FECHA  (MM/dd/yyyy)  7- Escalas y Clasificaciones  8- FECHA  (yyy/MM/dd)  9- FECHA (dd/MMM/yyyy)  10-FECHA (dd \de MMMM \de yyyy)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la Variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'VARIABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del grupo de parametros otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'IDGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';

