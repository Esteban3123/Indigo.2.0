''' <summary>
''' Esta enumeración permite establecer el icono que acompañara
''' al xtraMessage en la barra botones
''' </summary>
Public Enum ImagesXtraLabel

    ''' <summary>
    ''' Información
    ''' </summary>
    Info = 0
    ''' <summary>
    ''' Advertencia
    ''' </summary>
    Warning = 1

End Enum

''' <summary>
''' Esta enumeracion se refiere al tipo de accion a realizar en la barra
''' Nuevo - Agrega un nuevo boton en el page
''' Adicionar - Agrega un link a un boton
''' </summary>
Public Enum eMenu
    Nuevo = 1
End Enum

''' <summary>
''' Lista de acciones para las cuales se
''' puede preparar la barra dependiendo de los permisos
''' </summary>
Public Enum eAction

    ''' <summary>
    ''' Ningún boton
    ''' </summary>
    None
    ''' <summary>
    ''' Habilita solo el botón de búsqueda en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    OnlyFind
    ''' <summary>
    ''' Habilita solo el botón de nuevo en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    OnlyNew
    ''' <summary>
    ''' Habilita el botón de nuevo y buscar en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    [New]
    ''' <summary>
    ''' Habilita solo el botón de actualizar en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    OnlyUpdate
    ''' <summary>
    ''' Habilita el botón de actualizar y buscar en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    Update
    ''' <summary>
    ''' Habilita solo el botón de eliminar en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    OnlyDelete
    ''' <summary>
    ''' Habilita el botón de eliminar y buscar en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    Delete
    ''' <summary>
    ''' Habilita solo el botón de procesos en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    OnlyProcess
    ''' <summary>
    ''' Habilita el page de procesos y botón buscar en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    Process
    ''' <summary>
    ''' Habilita solo el botón de deshacer en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    OnlyUndo
    ''' <summary>
    ''' habilita deshacer y auditoria y gestion documental
    ''' </summary>
    ''' <remarks></remarks>
    OnlyUndoAndAudit
    ''' <summary>
    ''' Habilita el botón de actualizar y procesos en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    UpdateOrProcess
    ''' <summary>
    ''' Habilita el botón nuevo y búscar en la
    ''' barra dependiendo de los permisos
    ''' </summary>
    NewAndFind
    ''' <summary>
    ''' Habilita solo el botón de guardar en la
    ''' barra dependiendo de los permisos
    ''' </summary>
    OnlySave
    ''' <summary>
    ''' Solo boton de guardar sin deshacer y buscar
    ''' </summary>
    OnlySaveWithoutUndoAndFind
    ''' <summary>
    ''' Habilita el page de guardar y botón buscar en
    ''' la barra dependiendo de los permisos
    ''' </summary>
    Save
    ''' <summary>
    ''' Habilita el botón actualizar y eliminar
    ''' en la barra dependiendo de los permisos
    ''' </summary>
    UpdateOrDelete
    ''' <summary>
    ''' Habilita el botón actualizar y procesos
    ''' en la barra dependiendo de los permisos
    ''' </summary>
    UpdateAndProcess
    ''' <summary>
    ''' Habilita el botón actualizar y procesos al igual que el de imprimir
    ''' en la barra dependiendo de los permisos
    ''' </summary>
    UpdateAndProcessWithPrint
    ''' <summary>
    ''' Habilita el botón guardar y procesos
    ''' en la barra dependiendo de los permisos
    ''' </summary>
    SaveAndProcess

    ''' <summary>
    ''' Habilita el boton de liquidar, con formulario de procesos que lo requieran
    ''' </summary>
    ''' <remarks></remarks>
    OnlyLiquidate
    ''' <summary>
    ''' Habilita el boton de liquidar, con formulario de procesos que lo requieran
    ''' </summary>
    ''' <remarks></remarks>
    OnlyConfirmLiquidate
    ''' <summary>
    ''' Habilita los botones de confirmar y confirmarTodos
    ''' </summary>
    ''' <remarks></remarks>
    OnlyAllConfirmLiquidate
    ''' <summary>
    ''' Habilita el boton de imprimir y deshacer 
    ''' </summary>
    OnlyUndoAndPrint
    ''' <summary>
    ''' Habilita el botón actualizar y eliminar
    ''' en la barra dependiendo de los permisos sin mostrar el boton de busqueda
    ''' </summary>
    OnlyUpdateOrDelete

    ''' <summary>
    ''' Oculta el boton de auditoria
    ''' </summary>
    ''' <remarks></remarks>
    OnlyHideAudit

    ''' <summary>
    ''' Oculta el boton de gestion Documental
    ''' </summary>
    ''' <remarks></remarks>
    OnlyHideDocumental

    ''' <summary>
    ''' Oculta los botones de accion dejando visible el boton de suspender
    ''' </summary>
    ''' <remarks></remarks>
    OnlySuspend

    ''' <summary>
    ''' Muestra el boton de generar archivo.
    ''' </summary>
    ''' <remarks></remarks>
    OnlyGenerateFile

    ''' <summary>
    ''' Muestra los botones de rejillas si tiene permisos
    ''' </summary>
    ''' <remarks></remarks>
    OnlyActionsGrid

    ''' <summary>
    ''' Muestra los botones de Guardar y guardar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    OnlySaveConfirm

    ''' <summary>
    ''' Muestra los botones de confirmar y anular
    ''' </summary>
    ''' <remarks></remarks>
    OnlyConfirmAnnular

    ''' <summary>
    ''' Muestra los botones de desconfirmar y anular
    ''' </summary>
    ''' <remarks></remarks>
    OnlyDisconfirmAnnular

    ''' <summary>
    ''' Muestra los botones de actualizar eliminar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    OnlyUpdateDeleteConfirm

    ''' <summary>
    ''' Muestra los botones de actualizar confirmar y anular
    ''' </summary>
    ''' <remarks></remarks>
    OnlyUpdateConfirmAnnular

    ''' <summary>
    ''' Muestra los botones de actualizar, (Actualizar y confirmar) y anular
    ''' </summary>
    ''' <remarks></remarks>
    OnlyUpdateConfirmIntegratedAnnular

    ''' <summary>
    ''' muestra  el boton de desconfirmar 
    ''' </summary>
    ''' <remarks></remarks>
    OnlyDisconfirm
    ''' <summary>
    ''' muestra solo el boton de deshacer y anular
    ''' </summary>
    ''' <remarks></remarks>
    OnlyUndoAnnular
    ''' <summary>
    ''' Solo muestra el control de navegación
    ''' </summary>
    OnlyNavigationControl

End Enum

''' <summary>
''' Enumeracion para saber el tipo de mascara que se va utilizar en IndigoTextEdit
''' </summary>
Public Enum EMask
    ''' <summary>
    ''' Ninguna mascara usar este tipo para personalizar la mascara del control
    ''' </summary>
    Ninguno = 0
    ''' <summary>
    ''' Mascara que permite el ingreso de valores alfanumericos y simbolos
    ''' </summary>
    AlfaNumerico = 1
    ''' <summary>
    ''' mascara de solo numeros del  0-9
    ''' </summary>
    Numerico = 2
    ''' <summary>
    ''' Mascara de tipo moneda sin decimales
    ''' </summary>
    Moneda = 3
    ''' <summary>
    ''' Mascara de tipo moneda con decimales
    ''' </summary>
    MonedaDecimales = 4
    ''' <summary>
    ''' Mascara numerico con dos decimales
    ''' </summary>
    NumericoDosDecimales = 5
    ''' <summary>
    ''' Mascara de solo letras con espacios mayusculas
    ''' </summary>
    SoloLetraMayuscula = 6
    ''' <summary>
    ''' Mascara de solo letras con espacios minusculas
    ''' </summary>
    SoloLetraMinuscula = 7
    ''' <summary>
    ''' Mascara de solo letras con espacios mayusculas y minusculas
    ''' </summary>
    SoloLetraMayusculaMinuscula = 8
    ''' <summary>
    ''' Mascara  para correo electronico ---@---.--
    ''' </summary>
    CorreoElectronico = 9
    ''' <summary>
    ''' mascara de porcentaje
    ''' </summary>
    Porcentaje = 10
    ''' <summary>
    ''' Mascara de telefono (---)-------
    ''' </summary>
    Telefono = 11
End Enum

''' <summary>
''' Lugar desde donde se manda a imprimir el reporte
''' </summary>
Public Enum PrintReportAction
    ''' <summary>
    ''' Al momento de crear un nuevo documento
    ''' </summary>
    Create
    ''' <summary>
    ''' Al momento de actualizar un documento
    ''' </summary>
    Update
    ''' <summary>
    ''' Al momento de confirmar un documento
    ''' </summary>
    Confirm
    ''' <summary>
    ''' Al momento de anular un documento
    ''' </summary>
    Cancel
    ''' <summary>
    ''' Al momento de dar click en el botón imprimir, para impresión directa
    ''' </summary>
    DirectPrinting
    ''' <summary>
    ''' Al momento de dar click en el botón imprimir, para visualizar el reporte
    ''' </summary>
    ViewPrinting
    ''' <summary>
    ''' Imprimie o visualiza el reporte según el perfil de impresión definido por el usuario
    ''' </summary>
    PrintProfile
    ''' <summary>
    ''' No tiene efecto
    ''' </summary>
    None
End Enum