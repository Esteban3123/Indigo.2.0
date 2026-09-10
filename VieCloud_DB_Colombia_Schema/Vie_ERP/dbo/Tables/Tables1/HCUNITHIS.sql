CREATE TABLE [dbo].[HCUNITHIS] (
    [CODCENATE]                        CHAR (10)    CONSTRAINT [DF_HCUNITHIS_CODCENATE] DEFAULT ((1)) NOT NULL,
    [UFUCODIGO]                        CHAR (10)    NOT NULL,
    [CODTIPHIS]                        CHAR (3)     NOT NULL,
    [CODBODEGA]                        VARCHAR (20) NOT NULL,
    [CODCENCOS]                        CHAR (14)    NOT NULL,
    [EXIHISING]                        CHAR (1)     NULL,
    [VISPROPLA]                        CHAR (1)     NULL,
    [VISSERPLA]                        CHAR (1)     NULL,
    [EXIINTRES]                        BIT          NULL,
    [EXDEVFENF]                        BIT          NULL,
    [EXHFENFEG]                        BIT          NULL,
    [EXHFENTRA]                        BIT          NULL,
    [EXEGRMEGE]                        BIT          NULL,
    [SOLMEDSIN]                        BIT          NULL,
    [REGINSSIN]                        BIT          NULL,
    [REGMEDSIN]                        BIT          NULL,
    [REGMEDSUS]                        BIT          NULL,
    [ESTSINRES]                        CHAR (1)     NULL,
    [PEREVOPAC]                        BIT          NULL,
    [SOLINSSIN]                        BIT          NULL,
    [PERDOSMAN]                        BIT          NULL,
    [PERFARINT]                        BIT          NULL,
    [PERFAREXT]                        BIT          NULL,
    [NUMHORFOR]                        INT          NULL,
    [PERFORADI]                        BIT          NULL,
    [INTMEDICA]                        BIT          NULL,
    [INTLABORA]                        BIT          NULL,
    [INTIMAGEN]                        BIT          NULL,
    [INTPATOLO]                        BIT          NULL,
    [INTPRNOQX]                        BIT          NULL,
    [INTPROCQX]                        BIT          NULL,
    [INTINTERC]                        BIT          NULL,
    [ABHCURGCE]                        BIT          NULL,
    [NUMHDESVE]                        CHAR (2)     NULL,
    [EVOULTFOL]                        BIT          NULL,
    [EVOHISING]                        BIT          NULL,
    [CONFOLESP]                        BIT          NULL,
    [NOTAEVOLU]                        BIT          NULL,
    [NOTEVOINT]                        BIT          NULL,
    [EVOHISINT]                        BIT          NULL,
    [INDAUDFOR]                        NUMERIC (18) NOT NULL,
    [VISADENFE]                        NCHAR (1)    NULL,
    [NOTADOWNT]                        BIT          NULL,
    [NOTARASSS]                        BIT          NULL,
    [PERMICTRL]                        BIT          NULL,
    [NOTANORTN]                        BIT          NULL,
    [VISESCNTN]                        BIT          NULL,
    [VISESCRAS]                        BIT          NULL,
    [VISESCDWN]                        BIT          NULL,
    [VISESCVAS]                        BIT          NULL,
    [NOTDIAOBL]                        TINYINT      NULL,
    [SOLMEINSU]                        CHAR (1)     NULL,
    [PERANUSER]                        CHAR (1)     NULL,
    [COPYPASTEMED]                     TINYINT      CONSTRAINT [DF_HCUNITHIS_COPYPASTEMED] DEFAULT ((1)) NOT NULL,
    [COPYPASTEENFER]                   TINYINT      CONSTRAINT [DF_HCUNITHIS_COPYPASTEENFER] DEFAULT ((1)) NOT NULL,
    [PERMODORME]                       TINYINT      NULL,
    [FACMECONINS]                      TINYINT      NULL,
    [HEMRES]                           TINYINT      CONSTRAINT [DF_HCUNITHIS_HEMRES] DEFAULT ((2)) NULL,
    [ALTAPUNTMINANES]                  BIT          NULL,
    [ESCALAALDRETEMOD]                 TINYINT      CONSTRAINT [DF_HCUNITHIS_ESCALAALDRETEMOD] DEFAULT ((2)) NOT NULL,
    [ESCALAALDRETE]                    TINYINT      CONSTRAINT [DF_HCUNITHIS_ESCALAALDRETE] DEFAULT ((2)) NOT NULL,
    [ESCALABROMAGE]                    TINYINT      CONSTRAINT [DF_HCUNITHIS_ESCALABROMAGE] DEFAULT ((2)) NOT NULL,
    [SOLICONFEHEMO]                    BIT          NULL,
    [PERMRECHEHEM]                     BIT          NULL,
    [RECMEDCHECK]                      BIT          NULL,
    [MAXCARACTERES]                    BIGINT       NULL,
    [NOTEVORN]                         BIT          NULL,
    [GENAINICIALURG]                   BIT          NULL,
    [PRIMERAVEZ]                       TINYINT      NULL,
    [VALIDAPREPARACIONMEZCLA]          BIT          NULL,
    [OXIGENOORDEN]                     BIT          NULL,
    [OXIGENOORDENSUS]                  BIT          NULL,
    [JUNTAMEDICA]                      BIT          NULL,
    [REGPCE]                           TINYINT      NULL,
    [REGPCEHF]                         TINYINT      NULL,
    [ACTPEHFCADA]                      INT          NULL,
    [HCFOLIOESPAMB]                    BIT          NULL,
    [DEFINIRBODEGAS]                   BIT          NULL,
    [MOSTRARINSUCERO]                  BIT          NULL,
    [SOLMEZLIQSINPRESCRIPCION]         BIT          NULL,
    [USUARIOCREACION]                  CHAR (20)    NULL,
    [FECHACREACION]                    DATETIME     NULL,
    [AllowDrugloans]                   BIT          NULL,
    [MinimumNumberHoursReplenishment]  INT          NULL,
    [AllowDiscountingNonStockMixtures] BIT          NULL,
    CONSTRAINT [PK_HCUNITHIS] PRIMARY KEY CLUSTERED ([CODCENATE] ASC, [UFUCODIGO] ASC, [CODTIPHIS] ASC),
    CONSTRAINT [FK_HCUNITHIS_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCUNITHIS_HCHISTORI] FOREIGN KEY ([CODTIPHIS]) REFERENCES [dbo].[HCHISTORI] ([CODTIPHIS]),
    CONSTRAINT [FK_HCUNITHIS_IHBODEGAS] FOREIGN KEY ([CODBODEGA]) REFERENCES [dbo].[IHBODEGAS] ([CODBODEGA]),
    CONSTRAINT [FK_HCUNITHIS_INCENTCOS] FOREIGN KEY ([CODCENCOS]) REFERENCES [dbo].[INCENTCOS] ([CODCENCOS]),
    CONSTRAINT [FK_HCUNITHIS_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCUNITHIS_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);




GO



GO



GO



GO



GO



GO



GO


CREATE trigger [dbo].[ActualizarCampodelAnexo2]
on [dbo].[HCUNITHIS]
for insert,update as
begin
			--UPDATE HCUNITHIS SET GENAINICIALURG = 1 where CODTIPHIS <> 'ENF'
	--UPDATE HCUNITHIS 
	--SET 
	--	HCUNITHIS.GENAINICIALURG = 1
	--FROM HCUNITHIS
	--	INNER JOIN  INUNIFUNC pc ON HCUNITHIS.UFUCODIGO = pc.UFUCODIGO
	--WHERE  CODTIPHIS <> 'ENF' AND UFUTIPUNI NOT IN ('15','24')
	select *from ADCENATEN where CODCENATE is null 

end
GO
DISABLE TRIGGER [dbo].[ActualizarCampodelAnexo2]
    ON [dbo].[HCUNITHIS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir descontar mezclas y liquidos sin existencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'AllowDiscountingNonStockMixtures';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de horas mínimas para la reposición de medicamentos en "Solicitud medicamentos e insumos" - Este parámetro determina el margen de horas para la sumatorio de productos entregados por farmacia en la columna de "Productos entregados"    importante para validar que la enfermeras no reponga cantidades superiores a la prescripción médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'MinimumNumberHoursReplenishment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para indicar si esta activo el prestamos de medicamentos por unidad funcional.    Nota: Este parametro lo lee el formulario de aplicacion de medicamentos.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'AllowDrugloans';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para identificar si permite registrar mezclas y líquidos sin prescripción médica en enfermeria, si esta en SI se habilita en enfermeria el boton nuevo para pode agregar una Mezcla/Liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'SOLMEZLIQSINPRESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mostrar insumos con cantidad en cero para la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'MOSTRARINSUCERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Definir Bodegas para mostrar cantidad de insumos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'DEFINIRBODEGAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Historia clinica desde un folio especifico para Ambulatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'HCFOLIOESPAMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Actualización oblig de plan enf en historia final cada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'ACTPEHFCADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define si se registra el plan de enfermeria en la historia final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'REGPCEHF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define si se registra plan de cuidado de enfermeria 1->SI 2->NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'REGPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Junta medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'JUNTAMEDICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se define si permite el registro de consumo de oxigeno con orden medica suspendida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'OXIGENOORDENSUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define si es obligatorio tener una orden médica para registrar un consumo de oxígeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'OXIGENOORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valida preparación mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'VALIDAPREPARACIONMEZCLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primera vez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'PRIMERAVEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0 - No genera Atencion Inicial de urgencia  1  - Genera Atencion Inicial de Urgencia    este parametro aplica solo para las unidades:      ''1:      Urgencias          ''2:      Hospitalizacion          ''5:     Unidades de Cuidado Intensivo Adulto          ''6:     Unidades de Cuidado Intermedio Adulto          ''7:     Unidades de Cuidado Intensivo Pediatrica          ''8:     Unidades de Cuidado Intermedio Pediatrica          ''9:     Unidades de Cuidado Intensivo Neonatal          ''10:     Unidades de Cuidado Intermedio Neonatal          ''11:    Unidades de Cuidado Basico Neonatal          ''19:     Cirugia          ''23:     Gineco -Obstetricia ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'GENAINICIALURG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para definir si se habilita nota rapida en la historia de un bebe en estancia conjunta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'NOTEVORN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' "Maximo de Caracteres Notas Evolución"para que se determine la cantidad de caracteres que se permitirá escribir en el campo "Objetivo-Analisis" del formato nota de evolución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'MAXCARACTERES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite mostrar las recomendaciones médicas por medio de Check', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'RECMEDCHECK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir rechazo de entrega de hemocomponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'PERMRECHEHEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solicitar confirmacion de entrega de hemocomponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'SOLICONFEHEMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1->Si habilitar Page  2->No Hablitar Page', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'ESCALABROMAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1->Si habilitar Page  2->No Hablitar Page', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'ESCALAALDRETE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Escala de Aldrete Modificada 1->Si habilitar Page  2->No Hablitar Page', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'ESCALAALDRETEMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No permitir dar alta sin puntuación mínima en alta de anestesia  0->No Marcado  1->Marcado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'ALTAPUNTMINANES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si tiene permiso para dar egreso a un paciente con hemocomponentes,   1-> No Permitir Alta Medica  2-> Descartar Reserva por Egreso de Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'HEMRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Facturar Medios de Contraste e Insumos: 1 Generar Solicitud de Dispensación 2 Facturar Productos Como Servicios 3 Por selección del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'FACMECONINS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite modificar ordenes médicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'PERMODORME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si tiene permiso para copiar y pegar en los richtextedit de     Dashboard Enfermeria, 1-> permite copiar y pegar 2-> No permite copiar y pegar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'COPYPASTEENFER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si tiene permiso para copiar y pegar en los richtextedit de     Dashboard Medicos, 1-> permite copiar y pegar 2-> No permite copiar y pegar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'COPYPASTEMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir Anular Servicios  1-Permitir Anular Servicios por la  misma Especialidad Solicitante  2-Permitir Anular solo por el Medico Solicitante  3-Permitir a Cualquiera  4-Ninguno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'PERANUSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solicitud de medicamentos e insumos  1-Cantidades Exactas en Ordenes Medicas  2-Advertir si no Coinciden con las Ordenes Medicas  3-Ninguno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'SOLMEINSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diligencia formatos de notificacion obligatoria diagnosticos  0: Exigir Siempre  1: Alertar para el Diligenciamiento  2: Ninguna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'NOTDIAOBL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Visibilidad del boton escala VAS en el dashboard paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'VISESCVAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Visibilidad del boton escala DownTon en el dashboard paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'VISESCDWN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Visibilidad del boton de la escala Ras en el dashboard del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'VISESCRAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Visibilidad del boton de la escala Norton en el dashboard del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'VISESCNTN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'GrabaNota en la escala norton 1:si  0:no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'NOTANORTN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir control en consulta externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'PERMICTRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parametro para permitir el registro de la nota de enfermeria para la escala Rass de forma automatica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'NOTARASSS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parametro para permitir el registro de la nota de enfermeria para la escala DownTon de forma automatica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'NOTADOWNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1. Exigir visado historia final enfermeria  2. Advertir la ausencia de visados en historia clinica final de enefermeria  3. Ninguno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'VISADENFE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CAmpo Auditoria  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Evolucion para Internos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'EVOHISINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nota de Evolucion para Internos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'NOTEVOINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nota de evolucion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'NOTAEVOLU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Continuar desde un Folio Especifico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'CONFOLESP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Evolución desde Historia Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'EVOHISING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Evolución desde el Ultimo Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'EVOULTFOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Maximo de Horas para Descontinuar Venopuncion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'NUMHDESVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 si el paciente ha seleccionado unidad destino Urgencias, de lo contrario se guarda 0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'ABHCURGCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deshabilitar Interfaz Interconsultas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'INTINTERC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deshabilitar Interfaz Procedimientos No Qx', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'INTPROCQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deshabilitar Interfaz Procedimientos Qx', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'INTPRNOQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deshabilitar Interfaz Patologias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'INTPATOLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deshabilitar Interfaz Imagenes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'INTIMAGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deshabilitar Interfaz Laboratorios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'INTLABORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deshabilitar Interfaz Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'INTMEDICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite formulacion adicional 1=Si;0=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'PERFORADI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' campo para numero de horas para la formulación intrahospitalaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'NUMHORFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite Indicaciones Farmacologicas en Plan de manejo Extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'PERFAREXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite Indicaciones Farmacologicas en Plan de manejo intrahospitalario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'PERFARINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite Dosis Manual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'PERDOSMAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir Registrar Solicitud de Insumos sin Cantidades Disponibles', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'SOLINSSIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parametro para permitir evolucionar pacientes de otras unidades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'PEREVOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estudios Sin Resultado:  1. No Permitir Alta Medica del Paciente  2. Anular Estudio Automaticamente a la Salida del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'ESTSINRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir Registrar Medicamentos Suspendidos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'REGMEDSUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir Descontar Medicamentos sin Cantidades Disponibles', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'REGMEDSIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir Registrar Gasto de Insumos sin Cantidades Disponibles', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'REGINSSIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir Registar medicamentos sin solicitud previa del Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'SOLMEDSIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exigir Alta Medica al Egreso en Hospitalizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'EXEGRMEGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exige Historia Final de Enfemeria al Traslado de Hospitalizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'EXHFENTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exige Historia Final de Enfemeria al Egreso de Hospitalizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'EXHFENFEG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exige Devolutivos en el formato de Historia Inicial y Final de Enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'EXDEVFENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exige Interpretacion de Paraclinicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'EXIINTRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mostrar Servicios IPS  1: Todos  2: Todos con Alerta cuando no esten cubiertos por el Plan  3: Solo los cubiertos en el Plan de Beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'VISSERPLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mostrar Productos de Inventarios  1: Todos  2: Todos con Alerta cuando no esten cubiertos por el Plan  3: Solo los cubiertos en el Plan de Beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'VISPROPLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exigir Historia de Ingreso por Unidad Funcional  1: Siempre exige Historia de Ingreso cuando cambia de Unidad Funcional    2: Permite de forma opcional Crear Historia de Ingreso por Unidad Funcional    3: No permite crear mas de una Hisoria de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'EXIHISING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ceontro de Costos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'CODCENCOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Bodega', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'CODBODEGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Tipo de Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'CODTIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración clínica y operativa por unidad funcional (piso, servicio, sala) y tipo de historia clínica: define qué módulos, permisos, integraciones y comportamientos están habilitados para cada unidad de atención dentro de un centro asistencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS';
