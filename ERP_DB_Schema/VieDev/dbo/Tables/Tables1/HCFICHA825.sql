CREATE TABLE [dbo].[HCFICHA825] (
    [ID]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT          NOT NULL,
    [CODDIAGNO]           CHAR (4)     NULL,
    [CONDICION]           BIT          NULL,
    [TIPOTUBER]           BIT          NULL,
    [LOCALTUBER]          INT          NULL,
    [SEGUANTECE]          BIT          NULL,
    [PREVTRAT]            INT          NULL,
    [PACTRASAL]           BIT          NULL,
    [OCUPACION]           VARCHAR (50) NULL,
    [PACCUENDIA]          BIT          NULL,
    [PESOACT]             VARCHAR (50) NULL,
    [TALLAACT]            VARCHAR (50) NULL,
    [IMC]                 VARCHAR (50) NULL,
    [BACILOS]             BIT          NULL,
    [RESULBACI]           BIT          NULL,
    [CULTIVO]             BIT          NULL,
    [RESUCULT]            BIT          NULL,
    [PRUEMOL]             BIT          NULL,
    [RESULPRUEMOL]        BIT          NULL,
    [NOMESPIDENT]         INT          NULL,
    [HISPATO]             BIT          NULL,
    [RESUHISPAT]          BIT          NULL,
    [RESPRUSENS]          BIT          NULL,
    [CUADCLI]             BIT          NULL,
    [NEXOEPI]             BIT          NULL,
    [RADIOL]              BIT          NULL,
    [ADA]                 BIT          NULL,
    [TUBERCULINA]         BIT          NULL,
    [COOMOR]              INT          NULL,
    [MONORES]             BIT          NULL,
    [MDR]                 BIT          NULL,
    [POLIRES]             BIT          NULL,
    [XDR]                 BIT          NULL,
    [RESRIFAMP]           BIT          NULL,
    [RESPREXDR]           BIT          NULL,
    [ESTREP1]             INT          NULL,
    [ESTREP2]             INT          NULL,
    [ISONIA1]             INT          NULL,
    [ISONIA2]             INT          NULL,
    [ISONIA3]             INT          NULL,
    [ETAMBU1]             INT          NULL,
    [ETAMBU2]             INT          NULL,
    [PIRAZI1]             INT          NULL,
    [PIRAZI2]             INT          NULL,
    [RIFAMP1]             INT          NULL,
    [RIFAMP2]             INT          NULL,
    [QUINDO1]             INT          NULL,
    [QUINDO2]             INT          NULL,
    [INYECT1]             INT          NULL,
    [INYECT2]             INT          NULL,
    [FECHACONFIR]         DATE         NULL,
    CONSTRAINT [PK_HCFICHA825] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFICHA825_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA825] NOCHECK CONSTRAINT [FK_HCFICHA825_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de confirmación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'FECHACONFIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'campo --> Resistencia a pre XDR - I: Inyectables  almacena  1=Sensible  2=Resistente    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'INYECT2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'campo --> XDR - I: Inyectables  almacena  1=Sensible    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'INYECT1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'campo --> Resistencia a pre XDR - Q: Quindonas  almacena  1=Sensible  2=Resistente    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'QUINDO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'campo --> XDR - Q: Quindonas  almacena  1=Sensible    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'QUINDO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'campo --> R: Rifampicina2 :      2=Resistente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RIFAMP2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'campo --> R: Rifampicina    almacena  1=Sensible  2=Resistente  3=No realizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RIFAMP1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'campo --> Z : Pirazinamida2       2=Resistente  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PIRAZI2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'campo --> Z : Pirazinamida1        1=Sensible  2=Resistente  3=No realizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PIRAZI1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'campo --> Etambul2         2=Resistente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ETAMBU2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'campo --> E: Etambutol         almacena  1=Sensible  2=Resistente  3=No realizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ETAMBU1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'campo --> Isoniazida3          almacena  1=Sensible  2=Resistente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ISONIA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'campo --> Isoniazida2       almacena   2=Resistente  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ISONIA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'campo --> Isoniazida - S: Estreptomicina  almacena  1=Sensible  2=Resistente  3=No realizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ISONIA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'campo --> Polirresistente - S: Estreptomicina  almacena  1=Sensible  2=Resistente  3=No realizado    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ESTREP2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'campo --> Monorresistencia - S: Estreptomicina  almacena  1=Sensible  2=Resistente  3=No realizado    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ESTREP1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Resistencia a pre XDR  1 = true   o = false ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESPREXDR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  RR (Resistencia a rifampicina)  1 = true   0 = false ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESRIFAMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  XDR  1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'XDR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Polirresistente   1= true   0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'POLIRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  MDR   1 = true    0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'MDR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Monorresistencia  1 = true    0 =false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'MONORES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'campo --> Coomorbilidades - condiciones especiales para el manejo  almacena     1=Diabetes  2=Silicosis  3=Enfermedad renal  4=EPOC  5=Enfermedad hepática  6=Cáncer  7=Artritis reumatoide  8=Desnutrición    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'COOMOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Tuberculina  1 = Si     0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'TUBERCULINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  ADA  1 =Si      0=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Radiológico  1 = Si   0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RADIOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Nexo epidemiológico   =Si   0 =No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'NEXOEPI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el  Cuadro clínico   1 =Si  0=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'CUADCLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'campo --> Resultado prueba de sensibilidad a fármacos (PSF)  almacena 1=true 0=false    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESPRUSENS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  Resultado hispatología   1 =  Positivo    0= Negativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESUHISPAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Hispatología   1 =  Si   0=  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'HISPATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la Especie Identificada:   1 = Tras Recaída   2 = Tras Fracaso    3 = Paciente Recuperado tras Pérdida al Seguimiento    4 = Otros Pacientes Previamente Tratados    5 = Tratamiento con Medicamento de 1ra Línea  6 =  Tratamiento con Medicamento de 2da Línea    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'NOMESPIDENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Resultado prueba molecular para confirmación del caso  1 =Positivo     0=Negativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESULPRUEMOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Prueba molecular   1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PRUEMOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  Resultado cultivo  1 =  Positivo  2  = Negativo  3 =  en proceso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESUCULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  el  Cultivo  1 =Si      0=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'CULTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  Resultado baciloscopia    1 = Positivo     0 =Negativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'RESULBACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Baciloscopia   1 = Si      0  = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'BACILOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  IMC (Índice masa corporal)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'IMC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Talla actual (Mts)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'TALLAACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Peso actual (Kg)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PESOACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'campo --> Paciente cuenta con diagnostico confirmado de vih ?   almacena 1=true 0=false    campo queda obsoleto con versionamiento V:03 2023-04-01 desde 27/10/2023   cambios pedidos en PBIs 12162, 12163 en esta columna se alamcenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PACCUENDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la ocuopación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'OCUPACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  ¿El paciente es trabajador de la salud?   1 =  Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PACTRASAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Previamente tratado  1 = tras recaída     2 =  tras fracaso   3 =Paciente Recuperado tras Pérdida al Seguimiento     4 =Otros Pacientes Previamente Tratados  5 = Tratamiento con Medicamento de 1ra Línea   6 = Tratamiento con Medicamento de 2da Línea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'PREVTRAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Según antecedente de tratamiento  1 = Nuevo    0=Previamente Tratado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'SEGUANTECE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Localización de la tuberculosis extrapulmonar  1 =Pleural   2 =Meningea   3 =Peritoneal  4 = Ganglionar   5 =Renal  6 =Intestinal  7 =Osteoarticular  8 =Genitourinaria   9 = Pericárdica  10 =Cutánea  11 =Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'LOCALTUBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el   Tipo de tuberculosis  1  = Pulmonar   0 = Extrapulmonar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'TIPOTUBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la condicion  1 = Sensible      0 = Resistente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'CONDICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA825', @level2type = N'COLUMN', @level2name = N'ID';

