CREATE TABLE [dbo].[HCORDPRON] (
    [IDETIPHIS]                     CHAR (9)                                                                           NOT NULL,
    [NUMEFOLIO]                     NCHAR (10)                                                                         NOT NULL,
    [IPCODPACI]                     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')   NOT NULL,
    [NUMINGRES]                     CHAR (10)                                                                          NOT NULL,
    [CODCENATE]                     CHAR (10)                                                                          NOT NULL,
    [UFUCODIGO]                     CHAR (10)                                                                          NOT NULL,
    [CODPROSAL]                     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')      NOT NULL,
    [FECORDMED]                     DATETIME                                                                           NOT NULL,
    [CODSERIPS]                     CHAR (20)                                                                          NOT NULL,
    [CANSERIPS]                     INT                                                                                NOT NULL,
    [OBSSERIPS]                     VARCHAR (2000)                                                                     NULL,
    [PRISERIPS]                     CHAR (1)                                                                           NOT NULL,
    [ESTSERIPS]                     CHAR (1)                                                                           NULL,
    [MANEXTPRO]                     BIT                                                                                NOT NULL,
    [CODDIAGNO]                     CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')       NULL,
    [EXREASITI]                     BIT                                                                                CONSTRAINT [DF_HCORDPRON_EXREASITI] DEFAULT ((0)) NOT NULL,
    [INDAUDFOR]                     NUMERIC (18)                                                                       NOT NULL,
    [AUTO]                          INT                                                                                IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GENSERVICEORDER]               INT                                                                                NULL,
    [NOMARCH]                       VARCHAR (250)                                                                      NULL,
    [TIPENFERME]                    TINYINT                                                                            NULL,
    [NUMDIALSEM]                    INT                                                                                NULL,
    [TIEMPSESION]                   INT                                                                                NULL,
    [UF]                            INT                                                                                NULL,
    [INTERPRET]                     VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "interpretation_Ofuscado", 0)') NULL,
    [CODPROINT]                     CHAR (20)                                                                          NULL,
    [NUMFOLINT]                     NCHAR (10)                                                                         NULL,
    [SOLICITASALA]                  BIT                                                                                NULL,
    [MEDREALI]                      CHAR (20)                                                                          NULL,
    [ESPEREALI]                     CHAR (3)                                                                           NULL,
    [FECHREALI]                     DATETIME                                                                           NULL,
    [TIPOFACTURACION]               INT                                                                                NULL,
    [IDRIASCUPS]                    INT                                                                                NULL,
    [CODVIAADMIN]                   CHAR (3)                                                                           NULL,
    [LITRXMINUT]                    DECIMAL (18, 2)                                                                    NULL,
    [TIPOOXIGENO]                   INT                                                                                NULL,
    [DURACION]                      INT                                                                                NULL,
    [UNIDADDURACION]                INT                                                                                NULL,
    [LATERALIDAD]                   INT                                                                                CONSTRAINT [DF__HCORDPRON__LATER__3EB89176] DEFAULT ((0)) NOT NULL,
    [IDDESCRIPCIONRELACIONADA]      INT                                                                                NULL,
    [CORRELACION]                   TINYINT                                                                            CONSTRAINT [DF__HCORDPRON__CORRE__7CABA24E] DEFAULT ((2)) NOT NULL,
    [OBSERVACIONCORRELA]            VARCHAR (4000)                                                                     NULL,
    [TraceabilityPaperworkEventsId] INT                                                                                NULL,
    [TraceabilityPaperworkId]       INT                                                                                NULL,
    [IDESQUEMAONC]                  INT                                                                                NULL,
    [IDADRADICACIONQX]              INT                                                                                NULL,
    [CODCENATEPRO]                  CHAR (10)                                                                          NULL,
    [UFUCODIGOPRO]                  CHAR (10)                                                                          NULL,
    [IDAREAPRO]                     INT                                                                                NULL,
    [CODUSUPRO]                     CHAR (20)                                                                          NULL,
    [FECHAPRO]                      DATETIME                                                                           NULL,
    [AUTOCITA]                      INT                                                                                NULL,
    [OBSANULADO]                    VARCHAR (300)                                                                      NULL,
    [PROANULADO]                    CHAR (20)                                                                          NULL,
    [FECHAANULADO]                  DATETIME                                                                           NULL,
    [CODMOTANU]                     CHAR (4)                                                                           NULL,
    CONSTRAINT [PK_HCORDPRON_1] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCORDPRON_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDPRON_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDPRON_Area] FOREIGN KEY ([IDAREAPRO]) REFERENCES [dbo].[HCAREASC] ([ID]),
    CONSTRAINT [FK_HCORDPRON_Cirugia] FOREIGN KEY ([AUTOCITA]) REFERENCES [dbo].[AGEPROGQX] ([CODAUTONU]),
    CONSTRAINT [FK_HCORDPRON_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]) ON UPDATE CASCADE,
    CONSTRAINT [FK_HCORDPRON_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCORDPRON_INESPECIA] FOREIGN KEY ([ESPEREALI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCORDPRON_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCORDPRON_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORDPRON_INPROFSAL1] FOREIGN KEY ([MEDREALI]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORDPRON_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCORDPRON_RIASCUPS] FOREIGN KEY ([IDRIASCUPS]) REFERENCES [dbo].[RIASCUPS] ([ID]),
    CONSTRAINT [FK_HCORDPRON_Schemes] FOREIGN KEY ([IDESQUEMAONC]) REFERENCES [EHR].[Schemes] ([Id]),
    CONSTRAINT [FK_PROANULADO] FOREIGN KEY ([PROANULADO]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [IX_HCORDPRON_1] UNIQUE NONCLUSTERED ([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [CODSERIPS] ASC, [MANEXTPRO] ASC, [IDDESCRIPCIONRELACIONADA] ASC)
);


GO
ALTER TABLE [dbo].[HCORDPRON] NOCHECK CONSTRAINT [FK_HCORDPRON_ADcenaten];


GO
ALTER TABLE [dbo].[HCORDPRON] NOCHECK CONSTRAINT [FK_HCORDPRON_ADINGRESO];


GO
ALTER TABLE [dbo].[HCORDPRON] NOCHECK CONSTRAINT [FK_HCORDPRON_Area];


GO
ALTER TABLE [dbo].[HCORDPRON] NOCHECK CONSTRAINT [FK_HCORDPRON_Cirugia];


GO
ALTER TABLE [dbo].[HCORDPRON] NOCHECK CONSTRAINT [FK_HCORDPRON_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCORDPRON] NOCHECK CONSTRAINT [FK_HCORDPRON_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCORDPRON] NOCHECK CONSTRAINT [FK_HCORDPRON_INESPECIA];


GO
ALTER TABLE [dbo].[HCORDPRON] NOCHECK CONSTRAINT [FK_HCORDPRON_INPacient];


GO
ALTER TABLE [dbo].[HCORDPRON] NOCHECK CONSTRAINT [FK_HCORDPRON_INPROFSAL];


GO
ALTER TABLE [dbo].[HCORDPRON] NOCHECK CONSTRAINT [FK_HCORDPRON_INPROFSAL1];


GO
ALTER TABLE [dbo].[HCORDPRON] NOCHECK CONSTRAINT [FK_HCORDPRON_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCORDPRON] NOCHECK CONSTRAINT [FK_HCORDPRON_RIASCUPS];


GO
ALTER TABLE [dbo].[HCORDPRON] NOCHECK CONSTRAINT [FK_HCORDPRON_Schemes];


GO
ALTER TABLE [dbo].[HCORDPRON] NOCHECK CONSTRAINT [FK_PROANULADO];


GO
CREATE NONCLUSTERED INDEX [HCORDPRON_CODCENATE_CODSERIPS_ESTSERIPS_MANEXTPRO_SOLICITASALA]
    ON [dbo].[HCORDPRON]([CODCENATE] ASC, [CODSERIPS] ASC, [ESTSERIPS] ASC, [MANEXTPRO] ASC, [SOLICITASALA] ASC)
    INCLUDE([NUMEFOLIO], [IPCODPACI], [NUMINGRES], [UFUCODIGO], [CODPROSAL], [FECORDMED], [PRISERIPS], [IDDESCRIPCIONRELACIONADA]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDPRON_ESTSERIPS_SOLICITASALA_CODCENATE_MANEXTPRO]
    ON [dbo].[HCORDPRON]([ESTSERIPS] ASC, [SOLICITASALA] ASC, [CODCENATE] ASC, [MANEXTPRO] ASC)
    INCLUDE([CANSERIPS], [CODDIAGNO], [CODPROSAL], [CODSERIPS], [FECORDMED], [IDDESCRIPCIONRELACIONADA], [IPCODPACI], [NUMEFOLIO], [NUMINGRES], [OBSSERIPS], [PRISERIPS], [UFUCODIGO]);


GO

 /* select * from Admission.AdmissionType */
create TRIGGER [dbo].[tgg_ActualizarCampo_ESTSERIPS] 
   ON  [dbo].[HCORDPRON]
   AFTER INSERT, UPDATE
AS 
BEGIN

	SET NOCOUNT ON;

	update HCORDPRON set ESTSERIPS = 2 from HCORDPRON i inner join inserted t on i.AUTO = t.AUTO where t.ESTSERIPS = 4
END

---update HCORDPRON set ESTSERIPS = 2 where ESTSERIPS = 4
GO
DISABLE TRIGGER [dbo].[tgg_ActualizarCampo_ESTSERIPS]
    ON [dbo].[HCORDPRON];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del motivo de anulacion (codigo de la tabla HCMOANULB) -->23-05-2025 - PBI 27070 Crear modal Justificacion anulación para las ordenes de Procedimientos no QX <--', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'fecha militar, que se llena cuando se anula el servicio desde dashboard de otros procedimientos. -->23-05-2025 - PBI 27070 Crear modal Justificacion anulación para las ordenes de Procedimientos no QX <--', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'FECHAANULADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'profesional de la salud, que se llena cuando se anula el servicio desde dashboard de otros procedimientos. -->23-05-2025 - PBI 27070 Crear modal Justificacion anulación para las ordenes de Procedimientos no QX <--', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'PROANULADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observacion cuando se anula el servicio desde dashboard de otros procedimientos. -->23-05-2025 - PBI 27070 Crear modal Justificacion anulación para las ordenes de Procedimientos no QX <--', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'OBSANULADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si se maneja correlación :   1-Si   2-No   3-Sin especificar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CORRELACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo de Lateralidad:   0 - No aplica   1 - Izquierda   2 - Derecha   3 - Ambos ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'LATERALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo se diligencia unicamente para servicio de tipo Oxigeno.  determina la unidad de la duracion  1) Dias  2) Meses    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'UNIDADDURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo se diligencia unicamente para servicio de tipo Oxigeno.  es una cantidad  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'DURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo se diligencia unicamente para servicio de tipo Oxigeno.    campo tipo lista desplegable con los valores 0- "Fijo" y 1 - "Continuo", debe  ser obligatorio sin postular ningún valor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'TIPOOXIGENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo se diligencia unicamente para servicio de tipo Oxigeno.  Listros por minuto  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'LITRXMINUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo se diligencia unicamente para servicio de tipo Oxigeno.  proviene del codigo de la tabla (Parámetros de Consumo Oxígeno)  (HCPARCONO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'CODVIAADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuando campo TIPO FACTURACION sea 1 guardamos  el IdRiasCups a la que aplica de lo contrario queda NULL  Relacion con la tabla RIASCUPS.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RIAS - Determina como se debe facturar el servicio:  1 - Rias  2 - Consulta Externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha realización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'FECHREALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda codigo de la especialidad que realiza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'ESPEREALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el CodigoProfesional o medico quien realiza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'MEDREALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Proceso de Dialisis:  UF campo de 4 digitos, no permite decimales  ultrafiltración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'UF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Proceso de Dialisis:  Tiempo de la sesion en horas, campo de 2 digitos, no permite decimales  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'TIEMPSESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Proceso de Dialisis:  Numero de dialisis por semana  campo de 2 digitos, no permite decimales  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'NUMDIALSEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Proceso de Dialisis:  Tipo de enfermedad: 1->Crónico, 2-> Agudo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'TIPENFERME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Servicio IPS  1: Ordenado  2: Completado  3: Interpretado  4: Sin Interfaz  5: Anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prioridad del Servicio Solicitado  1: Urgente  2: Rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPRON', @level2type = N'COLUMN', @level2name = N'PRISERIPS';

