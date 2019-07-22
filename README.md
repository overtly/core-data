### 项目层次说明

> Sodao.Core.Data v1.0.2

#### 1. 项目目录

```
|-Attrubute                     特性
|  |-SubmeterAttrubute.cs       分表标识特性
|
|-Contract                      契约层
|  |-IPropertyAssist.cs         统一的接口定义文件，所有的数据层接口均继承
|  |-IBaseRepository<T>.cs      基础接口类，包含增删改查，以及继承IDbRepository
|
|-DataContext                   数据库连接层
|  |-DataContext.cs             数据库连接工厂类
|  |-DataContextConfig.cs       数据库配置信息获取类
|
|-Enums                         枚举
|  |-DatabaseType.cs            数据库类型
|  |-FieldSortType.cs           Asc / Desc
|
|-Expressions                   表达式解析器（略）
|
|-Extensions                    Dapper扩展
|  |-Dapper.Extension.cs        扩展类
|
|-Params                        参数实体
|  |-OrderByField.cs            排序类
|
|-Repositry                     契约实现层
|  |-PropertyAssist.cs          抽象类，继承IPropertyAssist，virtual 
|  |-BaseRepository<T>.cs       抽象类，实现 IBaseRepository<T>，virtual
```


#### 2. 版本及支持

> * Nuget版本：V 1.0.3.3
> * 框架支持： Framework4.6 - NetStandard 2.0
> * 数据库支持：MySql / SqlServer / SQLite [使用详见下文]


#### 3. 项目依赖

> * Framework 4.6

```
Dapper 1.50.2  
MySql.Data 6.10.4
System.Data.SQLite 1.0.106
System.ComponentModel.DataAnnotations
```

> * NetStandard 2.0

```
Dapper 1.50.2  
MySql.Data 6.10.4
Microsoft.Data.Sqlite 2.0.0
```


### 使用

#### 1. 配置信息

> * 支持 IConfiguration 对象注入
> * 支持默认配置文件appsettings.json
> * Core(DbType=MySql|SqlServer): 

```
[mysql]: DataSource=10.0.17.10;Database=JuketoolCore;uid=juketool;pwd=abc@123;Allow Zero Datetime=True;DbType=MySql
[sqlserver]: Data Source=10.0.12.2;Initial Catalog=SD_SiteLog;Persist Security Info=True;User ID=mpass;Password=123456mpass;DbType=SqlServer  
[sqlite]: Data Source=chat.db;DbType=SQLite; // 默认使用App_Data目录
```

> * Framework: 正常的连接字符串，使用PrividerName来区分数据库类型


#### 2. Nuget包引用

```
Install-Package Sodao.Core.Data -Version 1.0.2.12
```


#### 3. 约定

> * 服务层契约需继承IBaseRepository<>
> * 服务层契约实现实现自定义契约，并继承BaseRepository<T>
> * 外部执行Sql使用 BaseRepository 中 Execute 方法 内部管理连接

```
return await Execute(async (connection) =>
{
    // 执行方法
}, true);
```


#### 4. Where表达式支持

> * ==
> * !
> * !=
> * null
> * In
> * Equals
> * Contains
> * StartWith
> * EndWith
> * &&
> * ||

##### 支持案例
```
var ary = new string[]{ "1", "2" };
var list = _repository.GetList(1, 1, oo=>ary.Contains(oo.UserId));

var ary = new List<string>(){ "1", "2" };
var list = _repository.GetList(1, 1, oo=>ary.Contains(oo.UserId));

var key = "abc";
var list = _repository.GetList(1, 1, oo=>oo.UserName.Contains(key));

var list = _repository.GetList(1, 1, oo=>oo.UserName.Equals(key));

var list = _repository.GetList(1, 1, oo=>oo.UserName.Contains("abc"));

var list = _repository.GetList(1, 1, oo=>oo.UserName.StartWith("abc"));

var list = _repository.GetList(1, 1, oo=>oo.UserName.EndWith("abc"));

var list = _repository.GetList(1, 1, oo=>ary.Contains(oo.UserId) && !IsSex);

var list = _repository.GetList(1, 1, oo=>oo.UserName != null);

var list = _repository.GetList(1, 1, oo=>oo.UserName == null);
```

##### 不支持案例

~~var list = _repository.GetList(1, 1, oo=>"test".Contains(oo.UserName));~~  
~~var list = _repository.GetList(1, 1, oo=>oo.UserName.IndexOf("abc") > -1);~~

---