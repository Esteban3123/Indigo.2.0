CREATE TABLE [dbo].[HCFARMEPD] (
    [CODCONCEC]                    NUMERIC (18)                                                                     NOT NULL,
    [IDETIPHIS]                    CHAR (9)                                                                         NULL,
    [NUMEFOLIO]                    NCHAR (10)                                                                       NULL,
    [IPCODPACI]                    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                    CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                    CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                    CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                    CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODPRODUC]                    CHAR (20)                                                                        NOT NULL,
    [DOSISPROD]                    NUMERIC (18, 2)                                                                  NULL,
    [CODUNIMED]                    VARCHAR (20)                                                                     NULL,
    [FRECUENCI]                    INT                                                                              NULL,
    [UNIFRECUE]                    CHAR (1)                                                                         NULL,
    [FECINIDOS]                    DATETIME                                                                         NULL,
    [TIPFORMED]                    CHAR (1)                                                                         NULL,
    [DURACIDOS]                    CHAR (20)                                                                        NULL,
    [VALDURFIJ]                    INT                                                                              NULL,
    [UNIDURFIJ]                    CHAR (1)                                                                         NULL,
    [CANPEDPRO]                    INT                                                                              NOT NULL,
    [CANENTPRO]                    INT                                                                              NOT NULL,
    [CANPENPRO]                    INT                                                                              NOT NULL,
    [PROESTADO]                    CHAR (1)                                                                         NOT NULL,
    [TIPOREGIS]                    CHAR (1)                                                                         NOT NULL,
    [JUSTIINSU]                    VARCHAR (MAX)                                                                    NULL,
    [RECIENACIDO]                  BIT                                                                              NULL,
    [NOPOSPROD]                    BIT                                                                              NULL,
    [ID]                           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDAGEPROGQX]                  INT                                                                              NULL,
    [EXTRAMURAL]                   BIT                                                                              NULL,
    [MEDICACUSTODIA]               BIT                                                                              NULL,
    [IDCITA]                       INT                                                                              NULL,
    [CONDITIONED]                  BIT                                                                              NULL,
    [UNIRS]                        BIT                                                                              NULL,
    [HCP]                          VARCHAR (MAX)                                                                    NULL,
    [SENDTO]                       TINYINT                                                                          CONSTRAINT [DF_HCFARMEPD_SENDTO] DEFAULT ((1)) NULL,
    [CodeSusceptibleMixingStation] UNIQUEIDENTIFIER                                                                 NULL,
    [VIEPROCESSED]                 TINYINT                                                                          CONSTRAINT [DF_HCFARMEPD_VIEPROCESSED] DEFAULT ((0)) NOT NULL,
    [SourceTable]                  VARCHAR (100)                                                                    NULL,
    [IdSourceTable]                INT                                                                              NULL,
    [CanceledQuantity]             INT                                                                              CONSTRAINT [DF_HCFARMEPD_CanceledQuantity] DEFAULT ((0)) NOT NULL,
    [Stat]                         BIT                                                                              CONSTRAINT [DF__HCFARMEPD__Stat__4B5E5F7D] DEFAULT ((0)) NULL,
    [IDAGPAQUETES]                 INT                                                                              NULL,
    [ProductType]                  INT                                                                              NULL,
    [FeedingsPerDay]               INT                                                                              NULL,
    [OuncesPerDose]                DECIMAL (4, 1)                                                                   NULL,
    [ConcentrationPerOunce]        DECIMAL (4, 1)                                                                   NULL,
    CONSTRAINT [PK_HCFARMEPD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFARMEPD_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCFARMEPD_AGPAQUETES] FOREIGN KEY ([IDAGPAQUETES]) REFERENCES [dbo].[AGPAQUETES] ([ID]),
    CONSTRAINT [FK_HCFARMEPD_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCFARMEPD] NOCHECK CONSTRAINT [FK_HCFARMEPD_ADINGRESO];


GO
ALTER TABLE [dbo].[HCFARMEPD] NOCHECK CONSTRAINT [FK_HCFARMEPD_AGPAQUETES];


GO
ALTER TABLE [dbo].[HCFARMEPD] NOCHECK CONSTRAINT [FK_HCFARMEPD_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IX_HCFARMEPD_NUMINGRES_CODPRODUC]
    ON [dbo].[HCFARMEPD]([NUMINGRES] ASC, [CODPRODUC] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCFARMEPD]
    ON [dbo].[HCFARMEPD]([CODCONCEC] ASC, [CODPRODUC] ASC, [CANPENPRO] ASC);


GO
CREATE NONCLUSTERED INDEX [nci_msft_1_HCFARMEPD_47145C98239FDE358ADA44D65013E8E4]
    ON [dbo].[HCFARMEPD]([CodeSusceptibleMixingStation] ASC, [CODPRODUC] ASC, [SENDTO] ASC)
    INCLUDE([CANPEDPRO], [CANPENPRO], [CODCONCEC], [IdSourceTable], [IPCODPACI], [PROESTADO]);


GO
CREATE NONCLUSTERED INDEX [IX_HCFARMEPD_Stat_CANPENPRO]
    ON [dbo].[HCFARMEPD]([Stat] ASC, [CANPENPRO] ASC)
    INCLUDE([CODCONCEC]);


GO
CREATE NONCLUSTERED INDEX [IX_HCFARMEPD__CODCONCEC__CODPRODUC__INC__ALL2]
    ON [dbo].[HCFARMEPD]([CODCONCEC] ASC, [CODPRODUC] ASC)
    INCLUDE([IDETIPHIS], [NUMEFOLIO], [IPCODPACI], [NUMINGRES], [CODCENATE], [UFUCODIGO], [CODPROSAL], [DOSISPROD], [CODUNIMED], [FRECUENCI], [UNIFRECUE], [FECINIDOS], [TIPFORMED], [DURACIDOS], [VALDURFIJ], [UNIDURFIJ], [CANPEDPRO], [CANENTPRO], [CANPENPRO], [PROESTADO], [TIPOREGIS], [JUSTIINSU], [RECIENACIDO], [NOPOSPROD], [ID], [IDAGEPROGQX], [EXTRAMURAL], [MEDICACUSTODIA]);


GO
CREATE NONCLUSTERED INDEX [IDX_HCFARMEPD_CODCONCEC_CANPENPRO_SENDTO]
    ON [dbo].[HCFARMEPD]([CODCONCEC] ASC, [CANPENPRO] ASC, [SENDTO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AdicionarSolicitado]
    ON [dbo].[HCFARMEPD]([IPCODPACI] ASC, [NUMINGRES] ASC, [CODPRODUC] ASC, [PROESTADO] ASC)
    INCLUDE([CANPENPRO]);


GO
CREATE NONCLUSTERED INDEX [IX_ViewDPD]
    ON [dbo].[HCFARMEPD]([CANPENPRO] ASC)
    INCLUDE([CANPEDPRO], [CODCONCEC], [CODPRODUC], [CODPROSAL], [CODUNIMED], [IDETIPHIS], [IPCODPACI], [NUMEFOLIO], [NUMINGRES], [PROESTADO], [TIPOREGIS]);


GO
CREATE NONCLUSTERED INDEX [IX_HCFARMEPD_PROESTADO_IPCODPACI_NUMINGRES_CODPRODUC_CODCONCEC_INC_CODCONCEC]
    ON [dbo].[HCFARMEPD]([PROESTADO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [CODPRODUC] ASC, [CODCONCEC] ASC)
    INCLUDE([CANPENPRO]);


GO
CREATE NONCLUSTERED INDEX [IX_ControlNoPOS]
    ON [dbo].[HCFARMEPD]([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [CODCENATE] ASC, [UFUCODIGO] ASC, [CODPRODUC] ASC)
    INCLUDE([CANENTPRO], [CANPENPRO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración por onza
---Habilitado y diligenciado solamente cuando el producto es de tipo componente lacteo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'ConcentrationPerOunce';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de onzas por dosis
---Habilitado y diligenciado solamente cuando el producto es de tipo componente lacteo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'OuncesPerDose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de tomas por día 
---Habilitado y diligenciado solamente cuando el producto es de tipo componente lácteo---', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'FeedingsPerDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de producto 
1. Medicamento 
2. Componente lácteo

Se realiza esta columna para distinguir los medicamentos de los componentes lácteos y de esa manera redirigir al dashboard respectivo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'ProductType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo contiene el identificador del paquete al que pertenece el producto, enlazando así los datos de esta tabla con la tabla AGPAQUETES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'IDAGPAQUETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el estado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'Stat';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la cantidad cancelada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'CanceledQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda aqui el ID de la Tabla Origen:    Ejemplos:    1ero: Si el medicamento ES guardado desde una HC el id origen seria el  de la tabla HCPRASCRA, si el medicamento viene de orden de  una mezcla el id origen que queda aqui seria el id origen de la tabla de la mezcla.    2. Cuando se haga una reposicion desde solicitud de medicamento e insumos y el medicamento que va a guardar NO cuenta con una prescripcion medica va el el ID de la tabla de la solicitud de medicamentos e insumos, PERO SI el medicamento SI cuenta con una prescrpcion medica va el ID de la tabla Origen es decir HCPRESCRA ó mezclas etc.        ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'IdSourceTable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda aqui el Nombre de la Tabla Origen:    Ejemplos:    1ero: Si el medicamento ES guardado desde una HC la tabla Origen seria HCPRASCRA si el medicamento viene de orden de  una mezcla seria el nombre que queda aqui seria el nombre de la tabla de la mezcla.    2. Cuando se haga una reposicion desde solicitud de medicamento e insumos y el medicamento que va a guardar NO cuenta con una prescripcion medica va el Nombre de la tabla de la solicitud de medicamentos e insumos, PERO SI el medicamento SI cuenta con una prescrpcion medica va el Nombre de la tabla Origen es decir HCPRESCRA.            ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'SourceTable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que se llena desde el Dasboard de Confirmacion de Dosis Unitarias ERP:     0 - Sin procesar  1 - Procesado Central de Mezclas  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'VIEPROCESSED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo identificador del agrupador por paquete', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'CodeSusceptibleMixingStation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo que especifica a donde se envía el registro:  
0 - Pendiente de enrutamiento  
1 - Enviar para procesar en dashboard farmacia  
2 - Enviar para procesar en central de mezclas  
NULL es similar al 1 en el aplicativo, es decir, se toma como procesar en dashboard farmacia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'SENDTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones que vienen desde central de mezclas del ERP, se asigna en el formulario de dashboard confirmación dosis unitarias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'HCP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el medicamento es UNIRS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'UNIRS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el medicamente es PBS condicionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'CONDITIONED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID cita de quimioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'IDCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que me identifica si el medicamento es de custodia, solo para la parte Intrahospitalaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'MEDICACUSTODIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registro Extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'EXTRAMURAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Tabla donde se guardan las programaiones de cirugía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el medicamento es NO POS:  True: NO POS  False: POS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'NOPOSPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina que el medicamento es para un recien nacido. medicamentos por defecto que se deben suministrar a un recien nacido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'RECIENACIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificacion Insumos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'JUSTIINSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Registro:  1. Medicamento  2. Materiales e Insumos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'TIPOREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Producto:  1. Pendiente  2. Entregado  3. Anulado por Suspencion del producto por parte del medico  4. Confirmacion de Entregado  5. Procesado por central de mezclas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'PROESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Pendiente de Entregar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'CANPENPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Entregada del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'CANENTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Pedida del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la duracion fija:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la duracion fija', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duracion de la Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'DURACIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Formulacion del medicamento:  1 Peso  2 Volumen  3 Peso-Volumen  4 Unidad de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'TIPFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'FECINIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la Frecuencia:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'FRECUENCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'DOSISPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

