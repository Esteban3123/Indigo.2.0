'***********************************************************************
' Assembly         : Presentation.Controls.MVP.IBusqueda
' Author           : WalterSierra
' Created          : 27-03-2011
'
' Last Modified By : WalterSierra
' Last Modified On : 27-03-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Definicion de Interfaz que contiene metodos y propiedades  para ser implementada en el formulario FrmBusqueda
''' </summary>
Public Interface IBusqueda

#Region "metodos"

    ''' <summary>
    ''' este metodo sirve para llamar el metodo obtener valores
    ''' </summary>
    ''' <remarks></remarks>
    Sub Aceptar()

    ''' <summary>
    ''' este metodo sirve para cerrar el frontal
    ''' </summary>
    ''' <remarks></remarks>
    Sub Cancelar()

    ''' <summary>
    ''' metodo utilizado para obtener los valores selccionados por el usuario, de acuerdo a las propiedades
    ''' ListadoSolicitud y
    ''' ValorSolicitado
    ''' </summary>
    Sub ObtenerValores()


#End Region

#Region "propiedades"


    ''' <summary>
    ''' la propiedad para manejar el origen de datos obtenido desde los servicios XPO, y establecidos en al regilla
    ''' </summary>
    ''' <value>el origen de datos que se va como datasource de la regilla.</value>
    WriteOnly Property OrigendeDatos As Object


    ''' <summary>
    ''' Obtiene el lisatdo del Orgen de Datos, Mediante una Enumeracion preestablecida
    ''' </summary>
    ''' <value>el origen de datos para la consulta de la busqueda.</value>
    Property ListadoOrigenDatos As Object


    ''' <summary>
    ''' establece el listado de fieldname solicitados
    ''' </summary>
    ''' <value>el listado de campos solicitados. _
    ''' cuando se necesite mas de uno, por ejemplo el codigo y el nombre _
    '''  Dim buscar As New FrmBusqueda _
    ''' Dim Solictados As New List(Of String) _
    ''' Solictados.Add("CodigoUsuario") _
    ''' Solictados.Add("NombreUsuario") _
    ''' buscar.ListadoSolicitud = Solictados</value>
    WriteOnly Property ListadoSolicitud As List(Of String)

    ''' <summary>
    ''' establece el listado de fieldname devueltos
    ''' </summary>
    ''' <value>el listado de campos devueltos._
    ''' Dim buscar As New FrmBusqueda _
    ''' Dim recibidos As New List(Of String) _
    ''' For Each item As String In buscar.ListadoDevolucion _
    '''     recibidos.Add(item) _
    '''     MessageBox.Show(item) _
    ''' Next</value>
    ReadOnly Property ListadoDevolucion As List(Of String)


    ''' <summary>
    ''' obtiene el valor seleccionado por el usuario
    ''' </summary>
    ''' <value>el valor seleccionado por el usuario._
    ''' Dim buscar As New FrmBusqueda _
    ''' txtCodigoUsuario.text = buscar.ValorDevuelto</value>
    ReadOnly Property ValorDevuelto As String

    ''' <summary>
    ''' Obtiene el valor del objeto en la fila seleccionada por el usuario
    ''' </summary>
    ''' <returns>Objeto de la fila seleccionada por el usuario</returns>
    ReadOnly Property ValorObjetoDevuelto As Object

    ''' <summary>
    ''' Nombre del campo solicitado (fieldname), si no se especifica se retorna el de la primera columna
    ''' </summary>
    ''' <value>el fieldname. _
    ''' Dim buscar As New FrmBusqueda _
    ''' buscar.ValorSolicitado = "CodigoUsuario"</value>
    WriteOnly Property ValorSolicitado As String
    ''' <summary>
    ''' Propiedad que tiene el filtro en una busqueda(caso departamentos y ciudades)
    ''' </summary>
    Property FiltroBusqueda As String

#End Region

End Interface
