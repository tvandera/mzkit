REM  Oracle.LinuxCompatibility.MySQL.CodeSolution.VisualBasic.CodeGenerator
REM  MYSQL Schema Mapper
REM      for Microsoft VisualBasic.NET 2.1.0.2569

REM  Dump @2018/3/16 13:47:47


Imports System.Data.Linq.Mapping
Imports System.Xml.Serialization
Imports Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes
Imports MySqlScript = Oracle.LinuxCompatibility.MySQL.Scripting.Extensions

Namespace TMIC.FooDB.mysql

''' <summary>
''' ```SQL
''' 
''' --
''' 
''' DROP TABLE IF EXISTS `food_taxonomies`;
''' /*!40101 SET @saved_cs_client     = @@character_set_client */;
''' /*!40101 SET character_set_client = utf8 */;
''' CREATE TABLE `food_taxonomies` (
'''   `id` int(11) NOT NULL AUTO_INCREMENT,
'''   `food_id` int(11) DEFAULT NULL,
'''   `ncbi_taxonomy_id` int(11) DEFAULT NULL,
'''   `classification_name` varchar(255) DEFAULT NULL,
'''   `classification_order` int(11) DEFAULT NULL,
'''   `created_at` datetime NOT NULL,
'''   `updated_at` datetime NOT NULL,
'''   PRIMARY KEY (`id`)
''' ) ENGINE=InnoDB AUTO_INCREMENT=920 DEFAULT CHARSET=utf8;
''' /*!40101 SET character_set_client = @saved_cs_client */;
''' 
''' --
''' ```
''' </summary>
''' <remarks></remarks>
<Oracle.LinuxCompatibility.MySQL.Reflection.DbAttributes.TableName("food_taxonomies", Database:="foodb", SchemaSQL:="
CREATE TABLE `food_taxonomies` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `food_id` int(11) DEFAULT NULL,
  `ncbi_taxonomy_id` int(11) DEFAULT NULL,
  `classification_name` varchar(255) DEFAULT NULL,
  `classification_order` int(11) DEFAULT NULL,
  `created_at` datetime NOT NULL,
  `updated_at` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=920 DEFAULT CHARSET=utf8;")>
Public Class food_taxonomies: Inherits Oracle.LinuxCompatibility.MySQL.MySQLTable
#Region "Public Property Mapping To Database Fields"
    <DatabaseField("id"), PrimaryKey, AutoIncrement, NotNull, DataType(MySqlDbType.Int64, "11"), Column(Name:="id"), XmlAttribute> Public Property id As Long
    <DatabaseField("food_id"), DataType(MySqlDbType.Int64, "11"), Column(Name:="food_id")> Public Property food_id As Long
    <DatabaseField("ncbi_taxonomy_id"), DataType(MySqlDbType.Int64, "11"), Column(Name:="ncbi_taxonomy_id")> Public Property ncbi_taxonomy_id As Long
    <DatabaseField("classification_name"), DataType(MySqlDbType.VarChar, "255"), Column(Name:="classification_name")> Public Property classification_name As String
    <DatabaseField("classification_order"), DataType(MySqlDbType.Int64, "11"), Column(Name:="classification_order")> Public Property classification_order As Long
    <DatabaseField("created_at"), NotNull, DataType(MySqlDbType.DateTime), Column(Name:="created_at")> Public Property created_at As Date
    <DatabaseField("updated_at"), NotNull, DataType(MySqlDbType.DateTime), Column(Name:="updated_at")> Public Property updated_at As Date
#End Region
#Region "Public SQL Interface"
#Region "Interface SQL"
    Private Shared ReadOnly INSERT_SQL As String = <SQL>INSERT INTO `food_taxonomies` (`id`, `food_id`, `ncbi_taxonomy_id`, `classification_name`, `classification_order`, `created_at`, `updated_at`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');</SQL>
    Private Shared ReadOnly REPLACE_SQL As String = <SQL>REPLACE INTO `food_taxonomies` (`id`, `food_id`, `ncbi_taxonomy_id`, `classification_name`, `classification_order`, `created_at`, `updated_at`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');</SQL>
    Private Shared ReadOnly DELETE_SQL As String = <SQL>DELETE FROM `food_taxonomies` WHERE `id` = '{0}';</SQL>
    Private Shared ReadOnly UPDATE_SQL As String = <SQL>UPDATE `food_taxonomies` SET `id`='{0}', `food_id`='{1}', `ncbi_taxonomy_id`='{2}', `classification_name`='{3}', `classification_order`='{4}', `created_at`='{5}', `updated_at`='{6}' WHERE `id` = '{7}';</SQL>
#End Region
''' <summary>
''' ```SQL
''' DELETE FROM `food_taxonomies` WHERE `id` = '{0}';
''' ```
''' </summary>
    Public Overrides Function GetDeleteSQL() As String
        Return String.Format(DELETE_SQL, id)
    End Function
''' <summary>
''' ```SQL
''' INSERT INTO `food_taxonomies` (`id`, `food_id`, `ncbi_taxonomy_id`, `classification_name`, `classification_order`, `created_at`, `updated_at`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');
''' ```
''' </summary>
    Public Overrides Function GetInsertSQL() As String
        Return String.Format(INSERT_SQL, id, food_id, ncbi_taxonomy_id, classification_name, classification_order, MySqlScript.ToMySqlDateTimeString(created_at), MySqlScript.ToMySqlDateTimeString(updated_at))
    End Function

''' <summary>
''' <see cref="GetInsertSQL"/>
''' </summary>
    Public Overrides Function GetDumpInsertValue() As String
        Return $"('{id}', '{food_id}', '{ncbi_taxonomy_id}', '{classification_name}', '{classification_order}', '{created_at}', '{updated_at}')"
    End Function


''' <summary>
''' ```SQL
''' REPLACE INTO `food_taxonomies` (`id`, `food_id`, `ncbi_taxonomy_id`, `classification_name`, `classification_order`, `created_at`, `updated_at`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');
''' ```
''' </summary>
    Public Overrides Function GetReplaceSQL() As String
        Return String.Format(REPLACE_SQL, id, food_id, ncbi_taxonomy_id, classification_name, classification_order, MySqlScript.ToMySqlDateTimeString(created_at), MySqlScript.ToMySqlDateTimeString(updated_at))
    End Function
''' <summary>
''' ```SQL
''' UPDATE `food_taxonomies` SET `id`='{0}', `food_id`='{1}', `ncbi_taxonomy_id`='{2}', `classification_name`='{3}', `classification_order`='{4}', `created_at`='{5}', `updated_at`='{6}' WHERE `id` = '{7}';
''' ```
''' </summary>
    Public Overrides Function GetUpdateSQL() As String
        Return String.Format(UPDATE_SQL, id, food_id, ncbi_taxonomy_id, classification_name, classification_order, MySqlScript.ToMySqlDateTimeString(created_at), MySqlScript.ToMySqlDateTimeString(updated_at), id)
    End Function
#End Region
Public Function Clone() As food_taxonomies
                  Return DirectCast(MyClass.MemberwiseClone, food_taxonomies)
              End Function
End Class


End Namespace
