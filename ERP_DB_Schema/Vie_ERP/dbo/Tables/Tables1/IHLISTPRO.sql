CREATE TABLE [dbo].[IHLISTPRO] (
    [CODPRODUC]       CHAR (20)       NOT NULL,
    [CODDCIMED]       CHAR (20)       NULL,
    [DESPRODUC]       CHAR (255)      NOT NULL,
    [NOPOSPROD]       BIT             NOT NULL,
    [TIPPRODUC]       CHAR (1)        NOT NULL,
    [MANCONPRO]       BIT             NOT NULL,
    [REGINVACT]       BIT             NOT NULL,
    [REGINVIMA]       CHAR (30)       NULL,
    [CODGRUFAR]       CHAR (20)       NULL,
    [CODVIAADM]       CHAR (20)       NULL,
    [CONCENMED]       CHAR (50)       NULL,
    [PRESENMED]       CHAR (160)      NULL,
    [CODFORMED]       CHAR (20)       NULL,
    [TIEESTMED]       INT             NULL,
    [TIPFORMED]       CHAR (1)        NULL,
    [PESTOTMED]       NUMERIC (18, 2) NULL,
    [CODUNIPES]       CHAR (20)       NULL,
    [VOLTOTMED]       NUMERIC (18, 2) NULL,
    [CODUNIVOL]       CHAR (20)       NULL,
    [CODUNIADM]       CHAR (20)       NULL,
    [CALCANAUT]       BIT             NULL,
    [ESPDILPRO]       BIT             NULL,
    [ABRPROMEZ]       CHAR (200)      NULL,
    [PROESTADO]       BIT             NOT NULL,
    [PROCONTRO]       BIT             NOT NULL,
    [TODASPATO]       BIT             NOT NULL,
    [MANLOCALI]       BIT             NOT NULL,
    [RETRASOGE]       BIT             NOT NULL,
    [CODUSUCRE]       CHAR (20)       NULL,
    [FECUSUCRE]       DATETIME        NULL,
    [CODUSUMOD]       CHAR (20)       NULL,
    [FECUSUMOD]       DATETIME        NULL,
    [JUSINMEDI]       BIT             NOT NULL,
    [CODNIVRIE]       VARCHAR (20)    NULL,
    [CODJUMEES]       BIT             NOT NULL,
    [ADVERTENC]       VARCHAR (MAX)   NULL,
    [POSOLOGIA]       VARCHAR (MAX)   NULL,
    [MEDTRAZA]        BIT             NULL,
    [MATOSTOSIN]      BIT             NULL,
    [MEDICAMENTONPT]  BIT             CONSTRAINT [DF_IHLISTPRO_MEDICAMENTONOT] DEFAULT ((0)) NOT NULL,
    [OSMOLARIDAD]     DECIMAL (18, 2) NULL,
    [DENSIDAD]        DECIMAL (6, 4)  NULL,
    [UNIDADMANEJO]    INT             NULL,
    [CONSUMPTION]     BIT             NULL,
    [OPTOMETRYDEVICE] BIT             CONSTRAINT [DF__IHLISTPRO__OPTOM__706FE89B] DEFAULT ((0)) NOT NULL,
    [AddedAutomatic]  BIT             CONSTRAINT [DF_IHLISTPRO_AddedAutomatic] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CODPRODUC] PRIMARY KEY CLUSTERED ([CODPRODUC] ASC),
    CONSTRAINT [FK_IHLISTPRO_INIVERIES] FOREIGN KEY ([CODNIVRIE]) REFERENCES [dbo].[INIVERIES] ([CODNIVRIE])
);


GO
ALTER TABLE [dbo].[IHLISTPRO] NOCHECK CONSTRAINT [FK_IHLISTPRO_INIVERIES];


GO
CREATE NONCLUSTERED INDEX [IX_IHLISTPRO_PROESTADO_TIPPRODUC_CONSUMPTION]
    ON [dbo].[IHLISTPRO]([PROESTADO] ASC, [TIPPRODUC] ASC, [CONSUMPTION] ASC)
    INCLUDE([DESPRODUC], [JUSINMEDI]);


GO
CREATE NONCLUSTERED INDEX [IX_ESTATIPO]
    ON [dbo].[IHLISTPRO]([PROESTADO] ASC, [TIPPRODUC] ASC)
    INCLUDE([CODPRODUC], [DESPRODUC]);


GO
CREATE NONCLUSTERED INDEX [IX_MedPrescripciones]
    ON [dbo].[IHLISTPRO]([CODDCIMED] ASC, [ESPDILPRO] ASC, [PROESTADO] ASC, [TIPPRODUC] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '05-07-2023 se cra columna en la cual me va identificar si el producto fue creado de forma automatica desde la historia clinica por el medico. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'AddedAutomatic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si Dispositivo Optometría   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'OPTOMETRYDEVICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite saber si es de consumo, este campo se llena cuando es de item insumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CONSUMPTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de manejo, Campo que contiene la cantidad de descarga mínima a la que pretendemos manejar el producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'UNIDADMANEJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la densidad del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'DENSIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la osmolaridad del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'OSMOLARIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica si es un medicamento NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'MEDICAMENTONPT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica si el producto es material de ostosintesis.  solo puede ser "si", cuando el tipo de producto sea suministro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'MATOSTOSIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determinia si el medicamento es trazador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'MEDTRAZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Posologia que correspondiente a la ficha tecnica del producto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'POSOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Adverntencias correspondiente a la ficha tecnica del producto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'ADVERTENC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la justificacion de medicamentos especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODJUMEES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Nivel Riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODNIVRIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si exige justificacion de insumos / medicamentos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'JUSINMEDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Modificacion del registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'FECUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Ultimo Usuario que modifica el registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'FECUSUCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Realiza traslado de Sobrantes Si=1;No=3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'RETRASOGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Producto Maneja Localizacion 1: Si; 0:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'MANLOCALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el producto Pos aplica para todos las patologias del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'TODASPATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Producto de Control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'PROCONTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del medicamento:  True: Activo  False: Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'PROESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Abreviatura del Producto para mostrar en mezclas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'ABRPROMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el producto funciona como Diluyente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'ESPDILPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el sistema realiza un calculo basico automatico para determinar el numero de producto a solicitar segun la orden medica.    True: Calculo Basico  False: No Calcula    Nota: El sistema realiza dos tipos de calculo automatico de cantidades:    1. Complejo: Tomando como base, la duracion, dosis, frecuencia, presentacion (peso total o volumen total, o unidad de administracion) y la estabilidad del producto.    2. Basico: Tomando como base la duracion, dosis y frecuencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CALCANAUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unidad de Administracion  Nota: Opcion disponible para Cremas, Sobres, Tabletas Compuestas, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODUNIADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad de Medida del Volumen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODUNIVOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Volumen Total del Medicamento  Presentacion    Ejemplo:   Concentracion: 10mg / 2ml  Presentacion: 20 ml    Volumen Total =  20 ml     Nota: Cuando no aplique volumen ej: Tabletas, se deja Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'VOLTOTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad de Medida del Peso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODUNIPES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso Total del Medicamento  Concentracion X Presentacion    Ejemplo:   Concentracion: 10mg / 2ml  Presentacion: 20 ml    Peso Total =  (10 * 20 / 2)    Nota: Cuando no aplique peso ej: Tabletas Compuestas, se deja Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'PESTOTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Formulacion del medicamento:  1 Peso  2 Volumen  3 Peso-Volumen  4 Unidad de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'TIPFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Horas de estabilidad del medicamento a temperatura ambiente. En el caso de que el medicamento se encuentre expuesto a otras condiciones (Clima, Mezclado, etc) estas variables se almacenran desde el modulo de enfermeria y dichos valores se cargaran por el personal de enfermeria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'TIEESTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Forma Farmaceutica del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presentacion del Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'PRESENMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concentracion del Medicamento    Ej: 50mg / 5 ml - 500mg / Tab', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CONCENMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Via de Administracion Comun', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Grupo Farmacologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODGRUFAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registro INVIMA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'REGINVIMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define si el producto tiene registro INVIMA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'REGINVACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Es Producto de Control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'MANCONPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Producto  
1: Medicamentos  
2: Materiales e Insumos  
3: Medicamento disponible como Insumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'TIPPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el medicamento es NO POS:  True: NO POS  False: POS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'NOPOSPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Corta del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'DESPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Denominacion Internacion del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODDCIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Interno del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHLISTPRO', @level2type = N'COLUMN', @level2name = N'CODPRODUC';

