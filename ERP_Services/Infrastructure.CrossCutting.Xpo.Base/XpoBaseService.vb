' ***********************************************************************
' Assembly         : Infrastructure.CrossCutting.Xpo.Base
' Author           : Juan Diego Diaz
' Created          : 2014-01-03
' 
' Copyright        : (c) . All rights reserved.
' ***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports DevExpress.Data.Filtering

#End Region

''' <summary>
''' Clase abstracta para funciones bases de los servicios XPO
''' </summary>
Public MustInherit Class XpoBaseService

    ''' <summary>
    ''' Función para cargar una colección y retornar una lista especifica de datos
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="DataLayerAux">DataLayer para la conexión a traves del servicio XPO</param>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Lista tipo T</returns>
    Public Function LoadCollection(Of T)(DataLayerAux As IDataLayer, Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing, Optional session As Session = Nothing) As List(Of T)
        Dim sessionNew = Nothing
        If session IsNot Nothing Then
            sessionNew = session
        Else
            sessionNew = New Session(DataLayerAux)
        End If
        Dim classEntity As XPClassInfo = sessionNew.GetClassInfo(GetType(T))
        Dim ListObj = Nothing
        Using collection = New XPCollection(sessionNew, classEntity)
            If Fun Is Nothing Then
                If criteria Is Nothing Then
                    collection.Load()
                    ListObj = collection.OfType(Of T).ToList
                Else
                    collection.Criteria = CriteriaOperator.Parse(criteria)
                    Dim sortCollection As SortingCollection = New SortingCollection()
                    sortCollection.Add(New SortProperty(classEntity.KeyProperty.Name, DevExpress.Xpo.DB.SortingDirection.Descending))
                    collection.Sorting = sortCollection
                    collection.Load()
                    ListObj = collection.OfType(Of T).ToList
                End If
            Else
                collection.Load()
                ListObj = collection.OfType(Of T).Where(Fun).ToList
            End If
        End Using
        Return ListObj
    End Function

    Public Function LoadView(Of T)(DataLayerAux As IDataLayer) As XPView
        Dim sessionNew = New Session(DataLayerAux)
        Dim classEntity As XPClassInfo = sessionNew.GetClassInfo(GetType(T))
        Dim propertiesView = classEntity.PersistentProperties
        Dim assocProperties = classEntity.AssociationListProperties
        Dim ObjProperties = classEntity.ObjectProperties
        Dim xpViewObj As XPView = New XPView(sessionNew, classEntity)
        For Each item In propertiesView
            Dim propertyRef As Metadata.ReflectionPropertyInfo = item
            xpViewObj.AddProperty(propertyRef.MappingField, propertyRef.Name)
        Next
        For Each itemAssoc In assocProperties
            Dim propertyRef As Metadata.ReflectionPropertyInfo = itemAssoc
            For Each itemAssocAux In propertyRef.CollectionElementType.PersistentProperties
                Dim propertyRefAux As Metadata.ReflectionPropertyInfo = itemAssocAux
                xpViewObj.AddProperty(propertyRef.Name & "." & propertyRefAux.MappingField, propertyRef.Name & "[" & propertyRefAux.Name & "]")
            Next
        Next
        For Each itemObj In ObjProperties
            Dim propertyRef As Metadata.ReflectionPropertyInfo = itemObj
            Dim ClassAux = propertyRef.StorageType
            Dim classEntityAux As XPClassInfo = sessionNew.GetClassInfo(ClassAux)
            Dim propertiesViewAux = classEntityAux.PersistentProperties
            For Each itemObjAux In propertiesViewAux
                Dim propertyRefAux As Metadata.ReflectionPropertyInfo = itemObjAux
                xpViewObj.AddProperty(propertyRef.Name & "." & propertyRefAux.MappingField, propertyRef.Name & "." & propertyRefAux.Name)
            Next
        Next
        Return xpViewObj
    End Function

End Class
