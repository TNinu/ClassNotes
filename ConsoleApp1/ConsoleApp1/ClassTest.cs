using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Tools;

namespace ConsoleApp2 {
    using ConsoleApp1;
    static class CoutomClass
    {
        public static void Print(this Person1 person)
        {
            Console.WriteLine($"当前命名空间ConsoleApp2：{person.Name}");
        }
        public static void Print(this Person1 person,string name)
        {
            Console.WriteLine($"当前命名空间ConsoleApp2：{name}");
        }

    }
}
namespace ConsoleApp1
{
    using ConsoleApp2;
    using System.Data;
    using System.Reflection;

    #region 中介模式
    abstract class Mediator {
        //抽象中介类
        public abstract void Send(string message, Colleague colleague);
    }
    class ConcreateMediator : Mediator {
        //具体中介类
        public ConcreateColleague1 Colleague1 { get; set; }
        public ConcreateColleague2 Colleague2 { get; set; }

        public override void Send(string message, Colleague colleague)
        {
            if (colleague == Colleague1)
            {
                Colleague2.Notfiy(message);
            }
            else {
                Colleague1.Notfiy(message);
            }
        }
    }
    abstract class Colleague {
        //抽象同事类
        protected Mediator Mediator;
        public Colleague(Mediator mediator) {
            this.Mediator = mediator;
        }
        public abstract void Notfiy(string mes);
    }

     class ConcreateColleague1 : Colleague {
        //具体同事类
        public ConcreateColleague1(Mediator mediator) : base(mediator) { }
        public override void Notfiy(string mes)
        {
            Console.WriteLine($"同事1收到消息{mes}");
        }
    }
    class ConcreateColleague2 : Colleague
    {
        //具体同事类
        public ConcreateColleague2(Mediator mediator) : base(mediator) { }
        public override void Notfiy(string mes)
        {
            Console.WriteLine($"同事2收到消息{mes}");
        }
    }

    #endregion
    #region 状态更改
    class Context { 
        public State state { get; set; }
        public Context(State st) {
            this.state = st;
        }
        public void Request() {
            state.Handle(this);
        }
    }
    abstract class State {
        public abstract void Handle(Context context);
        
    }
    class AttackA : State
    {
        public override void Handle(Context context)
        {
            Console.WriteLine("一技能攻击");
            context.state = new AttackB(); 
        }
    }
    class AttackB : State
    {
        public override void Handle(Context context)
        {
            Console.WriteLine("二技能攻击");
            context.state = new AttackA();
        }
    }
    #endregion
    #region GC
    class SampeClass : IDisposable
    {
        //实现IDisposable接口，声明这是一个非托管类
        public void Dispose()
        {
            //阻止GC调用析构函数
            System.GC.SuppressFinalize(this);        
        }
    }
    #endregion
    #region 建造者模式
    //在一个类中实现需要实例化类的全部操作以减少客户端的操作
    abstract class PersonBulider {
        public abstract void BuliderHead();
        public abstract void BuliderHand();
        public abstract void BuliderBody();
        public abstract void BuliderLeg();
    }
    class BigPerson : PersonBulider
    {
        public override void BuliderBody(){}
        public override void BuliderHand(){}
        public override void BuliderHead(){}
        public override void BuliderLeg(){}
    }
    class SmallPerson : PersonBulider
    {
        public override void BuliderBody(){}
        public override void BuliderHand(){}
        public override void BuliderHead(){}
        public override void BuliderLeg(){}
    }
    class Director {
        private BigPerson big = new BigPerson();
        private SmallPerson sm = new SmallPerson();
        public void CreateBig() {
            big.BuliderBody();
            big.BuliderHand();
            big.BuliderHead();
            big.BuliderLeg();
        }
        public void CreateSmall() {
            sm.BuliderBody();
            sm.BuliderHand();
            sm.BuliderHead();
            sm.BuliderLeg();
        }
    }

    #endregion
    #region  抽象工厂模式
    interface IUser {
        void InsertUser();
        User GetUser(int id);
    }
    class SqlServerUser : IUser
    {
        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertUser()
        {
            throw new NotImplementedException();
        }
    }
    class User : IUser
    {
        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertUser()
        {
            throw new NotImplementedException();
        }
    }
    class AccessServerUser : IUser
    {
        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertUser()
        {
            throw new NotImplementedException();
        }
    }
    interface IFactory {
        IUser CreateUser();
        IDepartment CreateDepartment();
    }
    class SqlServerFactory : IFactory{
        public IDepartment CreateDepartment()
        {
            throw new NotImplementedException();
        }

        public IUser CreateUser() {
            return new User();
        }
     }
    interface IDepartment {
        IDepartment CreateDepartment();
    }
    class SqlserverDepartment : IDepartment
    {
        public IDepartment CreateDepartment()
        {
            return new SqlserverDepartment();
        }
    }
    class AccessDepartment : IDepartment
    {
        public IDepartment CreateDepartment()
        {
            return new AccessDepartment();
        }
    }

    #endregion
    #region 简单工厂模式
    class MonsterBase
    {
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Exp { get; set; }
        public virtual void Attack() { }
        public virtual void Hurt() { }

    }
    enum MonsterType { 
    Patrol,
    Fly,
    Boss
    }
    class MonsterFactory {
        public MonsterBase CreateMonster(MonsterType type) {
            switch (type)
            {
                case MonsterType.Patrol:
                    return new MonsterPatrol();
                case MonsterType.Fly:
                    return new MonsterFly();
                case MonsterType.Boss:
                    break;
                default:
                    break;
            }
            return null;
        }
    }
    class MonsterPatrol : MonsterBase {
        public override void Attack()
        {
            Console.WriteLine("Patrol开始攻击");
        }
        public override void Hurt()
        {
            
        }
    }
    class MonsterFly : MonsterBase
    {
        public override void Attack()
        {
            Console.WriteLine("Fly开始攻击");
        }
        public override void Hurt()
        {

        }
    }
    //工厂方法模式
    interface IMonster {
        MonsterBase CreateMonster();
    }
    class MonsterFlyFac : IMonster {
        public MonsterBase CreateMonster() {
            return new MonsterFly();
        }
    }
        
    #endregion
    #region 信息组装类
    class BasePacket { 
        public ushort Mes_id { get; set; }
        public ushort Mes_length { get; set; }
        public string Message { get; set; }
        public virtual byte[] GetBytes() { return null; }
    }
    class LoginPacket : BasePacket {
        public override byte[] GetBytes() { return null; }
    }
    #endregion
    #region 多线程、反射
    public delegate void DownFinishDel();
    //多线程
    class Multithreading {
       public static Queue<string> message = new Queue<string>();
        public static int ticke = 100;
        static object mylock = new object();  //互斥对象
        public static void Worker() {
            Thread.Sleep(1000);

            CallMethod();
            Console.WriteLine("子线程");
        }
        //子线程调用函数的时候，函数内部不能有刷新UI的操作
        public static void CallMethod() {
            Console.WriteLine("CallMethod");
        }
        public static void Worker1(object obj = null) {

            Console.WriteLine(obj as string);

            while (true)
            {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                string chatmessage = "asdfd";
                message.Enqueue(chatmessage);
                Thread.Sleep(1000);
            }
        
        }
        public static void SaleTicke1() {
            while (true)
            {
                try
                {
                    Monitor.Enter(mylock);
                    if (ticke > 0)
                    {
                        Console.WriteLine($"线程1取票{ticke--}");
                    }
                    else
                        break;
                }
                finally {
                    Monitor.Exit(mylock);
                }
                
            }
        }
        public static void SaleTicke2()
        {
            while (true)
            {
                try
                {
                    Monitor.Enter(mylock);
                    if (ticke > 0)
                    {
                        Console.WriteLine($"线程2取票{ticke--}");
                    }
                    else
                        break;
                }
                finally {
                    Monitor.Exit(mylock);
                }
                
            }
        }
        public static void DownFinishFile(object obj) {
            Thread.Sleep(3000);
            //下载完成
            DownFinishDel downFinishDel = obj as DownFinishDel;
            downFinishDel();

        }

    }
    class Singleton {
        //volatile 不允许线程缓存
        private static  object mylock = new object();
        private volatile static Singleton instance = null;
        Singleton() { }
        public static Singleton GetInatance {
            get {
                if (instance == null)
                {
                    lock (mylock)
                    {
                        if (instance == null)
                        {
                            instance = new Singleton();
                        }
                    }
                }
                return instance; 
            }
        }

    }
    //反射
    public class ReflectionClass { 
        public string a;
        public int b; 
        public int ID { get; set; } 
        public int Age { set; get; } 
        public ReflectionClass(string m, int n)
        { 
            Console.WriteLine("调用带参构造函数"); 
            a = m; 
            b = n;
        } 
        public ReflectionClass() 
        { 
            Console.WriteLine("调用无参默认构造函数"); 
        }
        public void Show() 
        { 
            Console.WriteLine("生成一个对象成功：" + this.ToString()); 
        } 
        public override string ToString() {
            return string.Format($"a:{a} b:{b} " +
                $"ID:{this.ID} Age:{this.Age}"); }

    }
    #endregion
    #region 代理、扩展、协变性
    interface IDelegate {
        void DoSomething();
    }
    class Entiy : IDelegate {
        DaiLi dl;
        public void DoSomething() {
       
            dl.DoSomething();
        }
        public void Set(DaiLi d) {
            dl = d;
        }
    }
    class DaiLi : IDelegate {
        public void DoSomething() {
            Console.WriteLine("我帮你去啊");
        }
    }
    //扩展
    class GameObject { 
        public string Name { get; set; }
        List<string> list = new List<string>();
        public string GetComponents(string name) {
            int index = list.IndexOf(name);
            if (index != -1)
            {
                return name;
            }
            return null;
        }
        public string AddComponents(string name) {
            if (!list.Contains(name))
            {
                list.Add(name);
            }
            return name;
        }
        public T GetComponents<T>(T name) {
            return name;
        }
    }
    static class GameObjectExtern {
        //第一个参数this，表示对哪个类进行扩展
        //第二个参数，才是调用时真正调用的函数
        public static string GetOrAddComponent(this GameObject obj,string name) {
            string com = obj.GetComponents(name);
            if (com == null)
            {
                com = obj.AddComponents(name);
            }
            return com;
        }
    }
    static class ListExtern {
        public static void s(this Structs a) { }
        public static int JSum(this IEnumerable<int> sourse) {
            if (sourse == null)
            {
                throw (new ArgumentNullException("数组为空"));
            }
            int jsum = 0;
            bool flag = false;
            foreach (var item in sourse)
            {
                if (!flag)
                {
                    jsum += item;
                    flag = true;
                }
                else
                    flag = false;
            }
            return jsum;
        }
        
    }
    struct Structs { }
    public class Person1 { 
        public string Name { get; set; }
       
    }
    static class ExtensionClass {
        public static void Print(this Person1 person) {
            person.Name = "啦啦啦啦啦啦啦";
            Console.WriteLine($"当前命名空间ConsoleApp1：{person.Name}");
        }

    }
    interface IDmon<out T> {
         T Method(string str);
    }
    interface IDmon1<in T>
    {
        string Method(T str);
    }
    class One : IDmon<string>, IDmon1<object>
    {
        public string Method(string str) {
            Console.WriteLine("one");
            return str;
        }

        public string Method(object str)
        {
            return null;
        }
    }
    class Two : IDmon<object>, IDmon1<object>
    {
        public object Method(string str)
        {
            Console.WriteLine("two");
            return str;
        }

        public string Method(object str)
        {
            return null;
        }
    }
    #endregion
    #region 迭代器
    class Friend {
        public string Name { get; set; }
        public Friend(string name) {
            this.Name = name;
        }
        public override string ToString()
        {
            return string.Format("Name:{0}",this.Name);
        }

    }
    class Friends : IEnumerable
    {
        private Friend[] friendArray;
        public int Length {
            get { return friendArray.Length; }
        }
        public Friends() {
            friendArray = new Friend[] { 
            new Friend("憨批刘海涛一号"),
            new Friend("憨批刘海涛二号"),
            new Friend("憨批刘海涛三号")
            };
        }
        public Friend this[int index] {
            get { return friendArray[index]; }
        }
        public int Count() {
            return friendArray.Length;
        }

        public IEnumerator GetEnumerator()
        {
            Console.WriteLine("自定义索引器");
            for (int index = 0; index < friendArray.Length; index++)
            {
                yield return this[index];
            }
        }
    }
    #endregion
    #region Partial
    public static class ExtensionClass1
    {
        public static void Print(this Person per)
        {
            //Console.WriteLine("Name{0}", per.Name);
        }
    }
   /* partial class Person { 
        public string Name { get; set; }
        public int Age { get; set; }
    }
    partial class Person { 
    
    }*/
    #endregion
    #region 泛型委托、泛型接口
    class Delegate_{
        public  delegate T Mydelegate<T>(T t1,T t2);
        public int Add(int t1,int t2) {
            return t1 + t2;
        }
        public string Add(string str1,string str2) {
            return string.Format(str1,str2);
        }
    }
    //泛型接口
    interface IBaseInterface<T> {
        void Show(T t);
    }
    interface IBaseInterface1<T,U> { }
    class SampleClass<T> : IBaseInterface<T> {
       public void Show(T t)
        {
            throw new NotImplementedException();
        }

    }
    class SampleClass1<T> : IBaseInterface<string> {
        public void Show(string t)
        {
            throw new NotImplementedException();
        }
    }
    class SampleClass2<T,U> : IBaseInterface1<T,string> { }
    class SortHelper<T> where T : IComparable {
        public void Sort(T[] array) {
            T temp;
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length - (i + 1); j++)
                {
                    if (array[j].CompareTo(array[j+1]) > 0)
                    {
                        temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }
    }
    abstract class TreeBase {
        public abstract int Energy();
    }
    class SoSoTree : TreeBase {
        public override int Energy() {
            return 1999;
        }
    }
    class ShaTree : TreeBase {
        public override int Energy()
        {
            return 1500;
        }
    }
    class People<T> where T : TreeBase {
       
        public string Name { get; set; }
        public int PeopleEnergy { get; set; }
        public void Plant(T tree) {
            if (PeopleEnergy < tree.Energy())
            {
                Console.WriteLine("能量不足，无法种植!");
            }
            else {
                PeopleEnergy -= tree.Energy();
                Console.WriteLine("种植成功！");
            }
        }
        public void Plant<U>(U tree) where U : TreeBase{
            if (PeopleEnergy < tree.Energy())
            {
                Console.WriteLine("能量不足，无法种植!");
            }
            else
            {
                PeopleEnergy -= tree.Energy();
                Console.WriteLine("种植成功！");
            }
        }

    }
    #endregion
    #region 泛型类、普通类中的泛型方法、约束
    //传进来的类型只有实现了IComparable里面的比较接口才能比较
    class Comparer<T, K> where T : IComparable {
        public T Code1 { get; set; }
        public K Code2 { get; set; }
        public static T CompareGeneric(T t1, T t2) {
            if (t1.CompareTo(t2) > 0)
                return t1;

            return t2;
        }
        
    }
    //普通泛型类
    class Stacka<T> {
        public void Print(T t) {
            Console.WriteLine(t.GetType());
        }
        public T Sum(T t1, T t2) {
            dynamic d1 = t1;
            dynamic d2 = t2;
            return d1 + d2;
        }
    }
    //静态泛型类
    static class GenericClass<T>
    {
        public static int Code { get; set; }
        static GenericClass()
        {
            Console.WriteLine("静态类被调用，参数类型为{0}",
                typeof(T));
        }
        public static void Print() { }
    }
        //静态泛型方法
    class GenericMethodClass {
        public static T GetMax<T>(T t1, T t2)where T : IComparable {
            if (t1.CompareTo(t2) > 0)
            {
                return t1;
            }
            return t2;
       }
    }
    //泛型类方法重载
    class Node<T,V> {
        public T Add(T t, V v) {
            return t;
        }
        public T Add(V v, T t) {
            return t;
        }
        public int Add(int a, int b) {
            return a + b;
        }
    }
    //继承字典
    class MyDictionary<T> : Dictionary<string, T> { 
    
    }
    //class表示泛型类型必须是引用类型
    class Boy<T> where T : class { 
    
    }
    //struct表示泛型类型必须是值类型
    class Boy1<T> where T : struct
    {

    }
    //基类约束、泛型类型必须是基类或其子类
    class Boy2<T> where T : Boy { }
    //接口约束
    interface IMove {
        void Move();
    }
    class WindowMove<T> where T : IMove{
       
    }
    //构造约束
    class WindowGz<T> where T : new() {
        public WindowGz() { }
    }
    #endregion
    #region 接口
    interface InMyInterface {
        int Age { get; set; }
        int ComapreTo(Object obj);
        void SayHello();
    }
    interface ISerialize {
        int WriteToFile();
        void SayHello();
    }
    class My : InMyInterface, ISerialize {
        public int Age { get; set; }
        public My() { }
        void InMyInterface.SayHello() {
            Console.WriteLine("你好");
        }
        void ISerialize.SayHello() {
            Console.WriteLine("Hello");
        }
        public My(int age) {
            this.Age = age;
        }
        public int ComapreTo(Object obj) {
            My my = (My)obj;
            if (this.Age > my.Age)
            {
                return 1;
            }
            else if (this.Age < my.Age)
            {
                return -1;
            }
            return 0;
        }

        public int WriteToFile()
        {
            return 0;
        }

    }
    #endregion
    #region 抽象
    public abstract class Fruit {
        //public abstract int age;  不能修饰字段
        public abstract float Price { get; }
        public abstract void GrowInArea();  //只声明不实现
        public void Display() { }   //可以有普通函数
    }
    public class Apple : Fruit {

        public override float Price { get; }
        public Apple(float price) {
            this.Price = price;
        }
        public override void GrowInArea() {
            Console.WriteLine("Apple's Price{0:c2}", this.Price);
        }
        public override bool Equals(object obj)
        {
            Apple other = (Apple)obj;
            return other.Price == this.Price;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(Apple a1, Apple a2) {
            return a1.Price == a2.Price;
        }
        public static bool operator !=(Apple a1, Apple a2)
        {
            return false;
        }
    }
    #endregion
    #region 继承、多态
    public class A { public A() { Console.WriteLine("A"); } }
    public class B : A { public B() { Console.WriteLine("B"); } }
    public class C : B { public C() { Console.WriteLine("C"); } }
    public class GunBase {
        public int age;
        public string Name { get; set; }
        public virtual void Shoot() {
            GCHandle handle1 = GCHandle.Alloc(this.Name);
            var pin1 = GCHandle.ToIntPtr(handle1);
            // Console.WriteLine("gunbase addr:{0}", pin1);

            //Console.WriteLine("GunBase shoot");
        }

    }
    class AK : GunBase {
        public override void Shoot()
        {


            Console.WriteLine("AK shoot");
        }
    }
    class AWM : GunBase
    {
        public override void Shoot()
        {
            Console.WriteLine("AWM shoot");
        }
    }
    class Animal {
        private string name;
        private int age;
        public static int h = 10;
        public Animal(string n) {
            this.name = n;
        }
        public string Name {
            get { return name; }
            set { name = value; }
        }
        public int Age {
            get { return age; }
            set { age = value; }
        }
        public void Voice() {
            Console.WriteLine("Animal Vioce");
        }
    }
    class Horse : Animal {
        
        //初始化子类对象，会先调用父类构造函数再调用自己的构造函数
        public Horse(string n) : base(n) {

        }

        //new的作用是隐藏父类的同名成员
        public new void Voice()
        {
            Console.WriteLine("Horse Vioce");
            base.Voice();   //可以调用父类的非私有函数和成员

        }
    }
    #endregion
    #region 类
    public class Point {
        public float X { get; set; }
        public float Y { get; set; }

        public Point() {
            this.X = 0.0f;
            this.Y = 0.0f;
        }
        public Point(float a, float b)
        {
            this.X = a;
            this.Y = b;
        }
        public void PointPrint() {
            //显示小数点后面两位并进位
            string point = string.Format("圆点坐标:({0:N},{1:N})", this.X, this.Y);
            Console.WriteLine(point);
        }
        public override string ToString()
        {
            return string.Format("圆点坐标:({0:N},{1:N})", this.X, this.Y);
        }

    }
    public class Circle {
        public Point Center { get; set; }
        public float Radius { get; set; }
        private int age;
        public int Age {
            get { return this.age; }
            set {
                if (value < 0 || value > 150)
                {
                    age = 18;
                    throw (new ArgumentOutOfRangeException("AgeIntProperty",
                        value, "年龄必须规范"));
                }
                else {
                    this.age = value;
                }
            }
        }
        public Circle() { }
        public Circle(Point p, float f, int age_) {
            //让Circle完全管理Point的生命周期
            this.Center = new Point(p.X, p.Y);
            this.Radius = f;
            this.Age = age_;
        }
        public void Draw() {
            Console.WriteLine("Radius:{0:N}", this.Radius);
            this.Center.PointPrint();
            Console.WriteLine("Age:{0}", this.age);
        }
        public double Area() {
            return Math.PI * Math.Pow(this.Radius, 2);
        }
        public double Length()
        {
            return Math.PI * this.Radius * 2;
        }

    }
    #endregion
    #region 向量
    public struct Vector
    {
        public int X;
        public int Y;
        public Vector(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public void Show()
        {
            Console.WriteLine("向量：({0},{1})", this.X, this.Y);
        }
        public static Vector operator +(Vector x, Vector y)
        {
            Vector c = new Vector();
            c.X = x.X + y.X;
            c.Y = x.X + y.Y;
            return c;
        }
        public static float operator *(Vector x, Vector y)
        {
            return x.X * y.X + x.Y * y.Y;
        }
        public static Vector operator -(Vector x, Vector y)
        {
            Vector c = new Vector();
            c.X = x.X - y.X;
            c.Y = x.X - y.Y;
            return c;
        }

    }
    #endregion
    #region 复数重载、运算符重载
    public struct Complex {
        public int real;
        public int xubu;
        public Complex(int r, int x) {
            this.real = r;
            this.xubu = x;
        }
        public void Show() {
            Console.WriteLine("复数：{0}+{1}i", this.real, this.xubu);
        }
        public static Complex operator +(Complex r, Complex x) {
            Complex c = new Complex();
            c.real = r.real + x.real;
            c.xubu = r.xubu + x.xubu;
            return c;
        }
        public static Complex operator *(Complex r, Complex x) {
            Complex c = new Complex();
            c.real = r.real * x.real + (-1) * r.xubu * x.xubu;
            c.xubu = r.real * x.xubu + r.xubu * x.real;

            return c;
        }
        public static Complex operator -(Complex r, Complex x) {
            Complex c = new Complex();
            c.real = r.real - x.real;
            c.xubu = r.xubu - x.xubu;
            return c;
        }

    }
    #endregion
    #region 类、结构体
    public struct Student {
        public string Name { get; set; }
        public int Age { get; set; }
        private const string country = "China";
        public static int number = 30;
        public DateTime Birthday {
            get { return DateTime.Now.AddYears(-20); }
        }
        public DateTime day;

        public Student(string name_, int age_) {
            this.Name = name_;
            this.Age = age_;
            this.day = DateTime.Now.AddYears(-20);
        }
        static Student() {
            Console.WriteLine("Static void");
        }

        public void Show() {
            Console.WriteLine(string.Format("name {0}", "age {1}", this.Name, this.Age));
        }
        public override string ToString()
        {
            string Info = string.Format("{0,-13};{1}\n" + "{2,-13};{3}\n"
                , "Name", this.Name, "Birthay", this.Birthday);

            return Info;

        }
        public void ShowTime() {
            Console.WriteLine(day);
        }
    }
    #region 单例模式
    class DataMar {
        //构造函数私有，外部无法进行new
        private DataMar() { }
        private static DataMar instance;
        public static DataMar Getinstance() {
            if (instance == null)
            {
                instance = new DataMar();
            }
            return instance;
        }
    }
    #endregion
    public class Person
    {
        public string name = "王二麻子";
        public int age = 22;
        private float height = 180.3f;
        private int score;
        protected bool sex = true;
        public const int code = 100; //不可更改
        //private readonly string county;//能在构造函数中进行初始化
        private static string h;

        public string Mode { get; set; } //相当于先定义成员变量在写属性
        //静态属性
        public static string H {
            get { return h; }
        }
        //静态构造函数，只调用一次，无参、无返回值
        static Person() {
            h = "sdfdf";
        }
        //构造函数重载
        public Person() { }
        public Person(string _name, int _age) : this() {
            this.name = _name;
            this.age = _age;
        }
        //属性   Property

        public string Name {
            set { name = value; }
            get { return name; }
        }
        public int Score {
            get { return score; }
            set { this.score = value; }
        }
        //析构函数
        ~Person() {
            Console.WriteLine("free");
        }
        public void Show()
        {
            Console.WriteLine("Name:{0}", this.name);
            Console.WriteLine("Age:{0}", this.age);
            Console.WriteLine("Height:{0}", this.height);

        }
        public override string ToString()
        {
            return string.Format("Age:{0}", this.age);

        }
    }
    public class Boy : Person
    {

    }
    public class CardManager {
        private string name;
        private int[] card = new int[54];
        private string[] str = new string[54];
        public CardManager(string name_) {
            this.name = name_;
        }
        public string Name { set { this.name = value; } get { return name; } }

        //索引
        public int this[int Index] {
            get { return card[Index]; }
            set { card[Index] = value; }
        }
        public string this[float Index] {
            get { return str[(int)Index]; }
            set { str[(int)Index] = value; }
        }
        public void Print()
        {
            Console.WriteLine("Name:{0}", this.Name);
        }

    }
    public class UIManager {

        Hashtable windows = new Hashtable();
        //索引
        public string this[string key] {
            get {
                if (windows.ContainsKey(key))
                    return windows[key].ToString();
                else
                    return "You Are So Stupid!";
            }
            set { windows[key] = value; }
        }
    }
    public class People
    {
        public string Name { set; get; }
        public People(string name_) {
            this.Name = name_;
        }
        public void Print()
        {
            Console.WriteLine(this.Name);
        }
        public override string ToString()
        {
            return string.Format("The Name is:{0}", this.Name);
            //return base.ToString();
        }
    }
    public class PeopleManager {
        People[] pm = new People[10];
        public People this[int index] {
            get { return pm[index]; }
            set { pm[index] = value; }
        }
        public static void ArrayAsPeople(People[] p) {
            People p1 = p[0];
            p1.Name = "Modify1";
            People p2 = p[1];
            p1.Name = "Modify2";
            Console.WriteLine(p1.Name);
            Console.WriteLine(p2.Name);
        }
    }
    #endregion
    #region 静态类
    public enum Gun_Type {
        Gun_AK,
        Gun_M416
    }
    public static class CreateGun {
        public static Gun GetCreateGun(Gun_Type at) {
            Gun gun = null;
            switch (at)
            {
                case Gun_Type.Gun_AK:
                    gun = new Gun_AK();
                    break;
                case Gun_Type.Gun_M416:
                    gun = new Gun_M416();
                    break;
                default:
                    break;
            }
            return gun;
        }
    }
    public class Gun {
        public const int sd = 1;

    }
    class Gun_AK : Gun { }
    class Gun_M416 : Gun { }

    #endregion
    #region OOP练习 window
    enum Window_Type { 
    HeroWindow,
    KillWindow,
    HomeWindow
    }
    public class BaseWindow {
        public BaseWindow() { }
        public bool Judge() {
            return true;
        }
        public virtual void SetActive(bool at) { }
    }
    class HeroWindow : BaseWindow {
        public override void SetActive(bool at)
        {
            Console.WriteLine("HeroWindow's LayOut");
        }
    }
    class HomeWindow : BaseWindow {
        public override void SetActive(bool at)
        {
            Console.WriteLine("HomeWindow's LayOut");
        }
    }
    class KillWindow : BaseWindow {
        public override void SetActive(bool at)
        {
            Console.WriteLine("KillWindow's LayOut");
        }
    }
    class WindowManager {

        Hashtable windows = new Hashtable();
        private WindowManager() { }
        public static WindowManager instance = null;
        public static WindowManager GetInatance() {
            if (instance == null)
            {
                instance = new WindowManager();
            }
            return instance;

        }
        public void OpenWindows(Window_Type type) {
            /* foreach (DictionaryEntry item in windows)
             {
                 if (item.Key.Equals(type))
                 {
                     ((BaseWindow)item.Value).LayOut();

                 }
             }*/
            if (windows.ContainsKey(type))
            {
                ((BaseWindow)windows[type]).SetActive(true);
            }
            else {
                AddWindows(type).SetActive(true);
            }
        }
        private void DeleteWindows(Window_Type type) {
            if (windows.ContainsKey(type))
            {
                windows.Remove(type);
            }
        }
        private BaseWindow AddWindows(Window_Type type) {
            BaseWindow basewindow = null; 
            switch (type)
            {
                case Window_Type.HeroWindow:
                    basewindow = new HeroWindow();
                    break;
                case Window_Type.KillWindow:
                    basewindow = new KillWindow();
                    break;
                case Window_Type.HomeWindow:
                    basewindow =  new HomeWindow();
                    break;
                default:
                    break;
            }
            windows.Add(type,basewindow);
            return basewindow;
        }
        public void CloseWindows(Window_Type type,bool isClear) {
            if (windows.ContainsKey(type))
            {
                ((BaseWindow)windows[type]).SetActive(false);
                if (isClear)
                {
                    DeleteWindows(type);
                }
            } 
        }
    }
    #endregion
    #region 值类型与引用类型练习
    class Ref { 
    public int Num { get; set; }
    }
    #endregion
    class ClassTest
    {
        #region 枚举
        enum GUN_TYPE
        {
            AK,
            M416,
            ROCECT
        }
        #endregion
        static void Main(string[] args)
        {
            #region Date
            //Day01(); /*2020.7.20*/
            //Day02(); /*2020.7.21*/
            //Day03(); /*2020.7.22*/
            //Day04(); /*2020.7.23*/
            //Day05(); /*2020.7.24*/
            //Day06(); /*2020.7.27*/
            //Day07(); /*2020.7.28*/
            //Day08(); /*2020.7.29*/
            //Day09(); /*2020.7.30*/
            //Day10(); /*2020.7.31*/
            //Day11(); /*2020.8.3*/
            //Day12(); /*2020.8.5*/
            //Day13(); /*2020.8.6*/
            //Day14(); /*2020.8.7*/
            //Day15(); /*2020.8.11*/
            //Day16(); /*2020.8.12*/
            //Day17(); /*2020.8.13*/
            //Day18(); /*2020.8.14*/
            Day19(); /*2020.8.20*/
            #endregion
            

        }
        #region byte
        static void Day01()
        {
            //Console.WriteLine("Hello World!");
            String num = Console.ReadLine();
            String num1 = "adjkj";
            Console.WriteLine(num);
            Console.WriteLine("num = {0}", num, "\n");
            int a = 10;   //变量：内存单元的别名
                          //变量命名法 ：驼峰、Pascal
            int @int = 10; //可以用但没必要
                           //一个字节占八位,相当于一个bit
                           //一个像素点占4字节(RGBA)
            int aa = 4;
            int get = Day01_test(10, 20);
            Console.WriteLine("The function return value's:{0}", get);
            Console.WriteLine(aa ^= 2);
            char cod = 'S';  //占2个字节
            long value_ = 12;  //8字节
            double dou = 1123; //8字节
            bool sy = true;    //1个字节
            byte b = 0xff;//16进制 10-15（abcdef）
            Console.WriteLine("hello" + b);  //不建议使用，hello字符串是const类型，
                                             //使用+修改了它的值
                                             //占位符
            Console.WriteLine("int := {0}", sizeof(int));
            Console.WriteLine("long := {0}", sizeof(long));
            Console.WriteLine("bool := {0}", sizeof(bool));
            Console.WriteLine("byte := {0}", sizeof(byte));
            Console.WriteLine("short := {0}", sizeof(short));

            Console.WriteLine("EnemyTag is:{0}", Define.EnemyTag);

            Day01_test(1, 2);


        }
        static int Day01_test(int num, int num1)
        {
            Console.WriteLine("Welcome to the function");
            int max = num + num1;
            return max;
        }
        #endregion
        #region int.Parse(),toString(),>>,<<
        static void Day02()
        {
            int a = 10;
            string str = "20";
            string stra = a.ToString();
            int strnum = Convert.ToInt32(str);
            Console.WriteLine("int -> string:{0}", stra);
            Console.WriteLine("string -> int:{0}", strnum);
            //string->float
            float fl = float.Parse(str);
            int num = int.Parse(str);
            Console.WriteLine("string->float:{0}", fl);
            Console.WriteLine("string->int:{0}", num);
            Console.WriteLine("input some string please:");
            String getstr = Console.ReadLine();
            Console.WriteLine("input an char please:");
            string getchar = Console.ReadLine();
            //Console.ReadKey();
            Console.WriteLine("your input string is:{0}---{1}", getstr, getchar); ;
            //字符串转整数/浮点数
            int.Parse(str);
            Convert.ToInt32(str);
            float.Parse(str);
            //整数转字符串
            num.ToString();
            //运算
            int firstnum = 20;
            int secondnum = firstnum / 2;
            int thirdnum = firstnum % 2;
            float f_num = (float)firstnum / 2;

            //if else
            string level = "";
            Console.WriteLine("输入成绩:");
            float score = float.Parse(Console.ReadLine());
            if (score > 90)
            {
                level = "A";
            }
            else if (score > 80)
            {
                level = "B";
            }
            else
            {
                level = "C";
            }
            Console.WriteLine("分数等级为:{0}", level);

            //for
            for (int i = 1; i < 10; i++)
            {
                Console.Write(" {0}", i);
            }
            Console.WriteLine("\n");
            //&& || !
            int ai = 12;
            int bi = 22;
            int ci = 23;
            if (ai + bi > ci && ai + ci > bi && bi + ci > ai)
            {
                Console.WriteLine("is Triangle");
            }
            else
            {
                Console.WriteLine("NOT");
            }
            //Console.WriteLine(1>2);
            string name = "";
            string pass = "";
            if (name.Length == 0 || pass.Length == 0)
            {
                Console.WriteLine("UserName or PassWord is Null!");
            }
            if (name.Length * pass.Length == 0)
            {
                Console.WriteLine("UserName or PassWord is Null!!");
            }
            //判断一个整数的二进制中1的个数,
            //方法一：右移与1逻辑与

            /* Console.WriteLine("输入一个整数：");
            uint number = uint.Parse(Console.ReadLine());
              Console.WriteLine("{0}的二进制中1的个数为：{1}",number,Day02_Chargeoneright(number));
             Console.WriteLine("{0}的二进制中1的个数为：{1}",number,Day02_Chargeoneleft(number));*/

            //前置后置++
            int m = 0, n = 0;
            m++;
            Console.WriteLine("m = {0}", m++);  //1
            n = ++m;
            Console.WriteLine("++n = {0}", ++n);  //4
            Console.WriteLine("n++ = {0}", n++);  //4

            //三目运算符
            int fnum = 10;
            int result = (fnum > 10) ? (fnum + 10) : (fnum - 10);
            Console.WriteLine("fnum = {0}", result);


        }
        static int Day02_Chargeoneright(uint v)
        {
            int count = 0;
            while (v > 0)
            {
                if ((v & 1) == 1)
                {
                    count++;
                }
                v >>= 1;
            }
            return count;
        }
        static int Day02_Chargeoneleft(uint v)
        {
            int flag = 1;
            int count = 0;
            while (flag < v)
            {
                if ((v & flag) == 1)
                {
                    count++;
                }
                flag <<= 1;
                // Console.WriteLine("flag :{0}",flag);
            }
            return count;
        }
        #endregion
        #region for,switch case,Random
        static void Day03()
        {

            //switch case
            string country = "Chain";
            switch (country)
            {
                case "Chain":
                    Console.WriteLine("你好"); break;
                case "USA":
                    Console.WriteLine("Hello"); break;
                default:
                    break;
            }
            //枚举类型
            GUN_TYPE gun = GUN_TYPE.M416;
            Console.WriteLine(gun);
            Console.WriteLine((GUN_TYPE)2);
            switch (gun)
            {
                case GUN_TYPE.AK:
                    break;
                case GUN_TYPE.M416:
                    break;
                case GUN_TYPE.ROCECT:
                    break;
                default:
                    break;
            }
            //while
            int i = 0;
            while (i < 20)
            {
                i += i;
                i++;
            }
            Console.WriteLine("i = {0}", i);
            Console.WriteLine("------------------");
            //9*9乘法表
            for (int x = 1; x < 9; x++)
            {
                for (int y = 1; y <= x; y++)
                {
                    Console.Write("{0}*{1}={2} ", y, x, x * y);
                }
                Console.WriteLine();
            }
            Console.WriteLine("------------------");
            //continue、break、return
            int jj = 0;
            while (true)
            {
                if (jj == 0)
                {
                    for (; ; )
                    {
                        Console.Write("j={0} ", jj);
                        if (jj == 5)
                            break;
                        jj++;
                    }
                }
                Console.Write("j={0} ", jj);
                if (jj == 10)
                    // return;
                    break;
                jj++;

            }
            Console.WriteLine();

            for (int q = 1; q < 10; q++)
            {
                for (int qq = 0; qq < 10; qq++)
                {
                    if ((q * qq) % 2 == 0)
                    {
                        Console.Write("q={0}、qq={1} ", q, qq);
                    }

                }
            }
            Console.WriteLine();

            string str1 = "hello world";
            string str2 = "hello world";
            if (str1.Equals(str2))
            {
                Console.WriteLine("str1 equal str2");
            }
            if (string.Compare(str1, str2) == 0)
            {
                Console.WriteLine("str1 equal str2");
            }
            //数组
            int[] array = new int[10];
            int[] array1 = new int[9];
            int ff = 0;
            int ee = 2;
            while (ff < array.Length)
            {
                array[ff] = ee;
                ee++;
                ff++;
            }
            int fd = 0;
            int fd1 = 0;
            while (fd < array1.Length)
            {
                if (fd == 5)
                {
                    fd++;
                    continue;
                }
                array1[fd1] = array[fd];
                fd++;
                fd1++;
            }
            // Console.WriteLine("Array Lenth:{0}",array.Length);
            int w = 0;
            while (w < array1.Length)
            {
                Console.Write("{0} ", array1[w]);
                w++;
            }
            int[] array3 = new int[10];
            int ff1 = 0;
            int ee1 = 1;
            while (ff1 < array3.Length)
            {
                array3[ff1] = ee1;
                ee1++;
                ff1++;
            }
            Console.WriteLine();

            for (int ii = 0; ii < array3.Length; ii++)
            {
                if (ii >= 5 && ii < array3.Length - 1)
                {
                    array3[ii] = array3[ii + 1];
                }
            }
            array3[array3.Length - 1] = 0;
            foreach (int item in array3)
            {
                Console.Write(item);
            }

            //字符型数组
            string[] sArray = new string[5] { "Monday","Tuesday","Wedneday","Tursday"
            ,"Friday"};

            //2DArray
            int[,] Twoarray = new int[3, 4];
            for (int iw = 0; iw < 3; iw++)
            {
                for (int iy = 0; iy < 4; iy++)
                {
                    Twoarray[iw, iy] = iw * iy;
                }
            }
            Console.WriteLine();
            Console.WriteLine(Twoarray[1, 3]);

            //随机数,左闭右开
            Random random = new Random();

            Console.WriteLine();
            //洗牌
            int[] card = new int[54];
            //int c = random.Next(0,54);
            int iww = 0;
            while (iww <= 53)
            {
                card[iww] = iww;
                iww++;
            }

            for (int ir = 1; ir <= 200; ir++)
            {
                int c = random.Next(0, 54);
                int c1 = random.Next(0, 54);
                if (c != c1)
                {
                    Swap(ref card[c], ref card[c1]);
                    /* tmp = card[c];
                     card[c] = card[c1];
                     card[c1] = tmp;*/
                }
            }
            foreach (int item in card)
            {
                Console.Write("{0} ", item);
            }
            //ArrayList
            Console.WriteLine();
            ArrayList list = new ArrayList();
            list.Add(10);
            list.Add(11);
            list.Add("hello");
            list.Insert(1, 30);
            list.Remove("hello");
            Console.WriteLine("30 IndexOf :{0}", list.IndexOf(30));
            foreach (var item in list)
            {
                Console.Write(item + " ");
            }

        }
        static int Add(int a, int b)
        {

            return a + b;
        }
        #endregion
        private static void Swap(ref int a, ref int b)
        {

            int tmp = a;
            a = b;
            a = tmp;
        }
        #region string, ArrayList,Hashtable,Stringbuilder
        static void Day04()
        {
            //删除相同元素的下标问题
            Day04_DeleteArrayList();
            //哈希表

            Day04_TestHash();
            //String 
            Day04_TestString();
            //StringBuilder
            Day04_TestStringBuilder();
            //TocharArray
            Day04_TestTocharArray();
        }
        private static void Day04_DeleteArrayList()
        {
            ArrayList list = new ArrayList();
            list.Add(12);
            list.Add(13);
            list.Add(15);
            list.Add(15);
            //从前往后删
            for (int i = 0; i < list.Count; i++)
            {
                if ((int)list[i] % 5 == 0)
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
            //从后往前删
            for (int i = list.Count - 1; i > 0; i--)
            {
                if ((int)list[i] % 5 == 0)
                {
                    list.RemoveAt(i);
                }
            }

            foreach (var item in list)
            {
                Console.Write(item + " ");
            }
        }
        private static void Day04_TestHash()
        {
            Hashtable table = new Hashtable();
            table.Add(1, "qwe");
            table.Add(2, "sssd");
            table.Add(3, "ddd");
            if (table.ContainsKey(1))
            {
                Console.Write("zhangsan ");
                Console.WriteLine(table["1"]);
            }
            //遍历Key
            Console.Write("Key : ");
            foreach (var item in table.Keys)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            //遍历Value
            Console.Write("Value : ");
            foreach (var item in table.Values)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            //遍历键值对
            foreach (DictionaryEntry item in table)
            {
                Console.Write(item.Key + ":" + item.Value + "  ");
            }
        }
        private static void Day04_TestString()
        {
            Console.WriteLine();
            string level = "The Most Level 爱上帝";
            //长度
            Console.WriteLine("String's length: {0}", level.Length);
            //下标访问
            Console.WriteLine(level[3]);
            //字符串是const类型，不能改变里面的内容
            //level[3] = 'A';   ×
            //字符串拼接函数
            int score = 100;
            string NewStr = string.Format("当前得分：{0}", score);
            Console.WriteLine(NewStr);
            //Join函数(字符拼接字符串函数)
            string[] con = new string[] { "刘", "海", "涛", "憨", "批" };
            string getstr = string.Join("-", con);
            Console.WriteLine(getstr);
            //函数重载：函数名一样，参数列表、参数个数、参数类型不一样，
            //返回类型不作为函数重载的一部分。

            //字符串比较函数
            string str1 = "hello world and you";
            string str2 = "hello world and you we";
            if (string.Compare(str1, str2) == 0)
            {
                Console.WriteLine("The str1 is same as the srt2");
            }
            else
            {
                Console.WriteLine("Not");
            }
            //字符串包含
            if (str1.Contains("hello"))
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }


            string ler = @"第二十二章 风卷决
阿萨的价钱低hi哦请问到后期和地区啊
##
第二十三章 争抢
阿杜覅护肤上的覅东方
##
第二十四章 一切待续
爱上帝啊第哦啊回踩哦
##
第二十五章 钱由我出
阿里耷拉红i代号为低
##
第二十六章 苦修
啊收到了拉动i阿德后
##
第二十七章 冲击第七段
啊看到的话抢我的覅哈佛i好气哦
##
第二十八章 强化“吸掌”
阿穹腭放大器和附件二哦i饭后服
##
第二十九章 重要的日子
是否会我i俄方还未发货无法
##
第三十章 辱人者，人恒辱之
阿鲁迪接哦附件二否符合未婚夫
##
第三十一章 一星斗者
啊啊哇顶起我i的趣味活动i请和我覅
##
第三十二章 挑战
奥兰多可能去我的好奇而佛前合肥
##
第三十三章 证实
安抚去哦覅强化肌肤琼海佛i全额付
##
第三十四章 翻身
阿尔派风景区佩服急切盼复进去哦
##
第三十五章 罪恶感
安抚看来你强迫我i飞机琼恩放进去
##
第三十六章 滑稽的突破
按实际宽度Casio都哈佛覅u去
##";
            string[] getcr = ler.Split("##");
            foreach (var item in getcr)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("请输入你要查看的章节：");
            int getinput = int.Parse(Console.ReadLine());
            Console.WriteLine(getcr[getinput - 1]);
            string Contentstr = @"1,2,3,4,5
3,4,5,1,3
4,5,2,4,4
2,4,5,2,5";
            string[] rowtcon = Contentstr.Split("\r\n");
            string[] plancon = rowtcon[0].Split(',');
            string[,] total = new string[rowtcon.Length, plancon.Length];
            for (int i = 0; i < rowtcon.Length; i++)
            {
                string[] col = rowtcon[i].Split(',');
                for (int j = 0; j < plancon.Length; j++)
                {
                    total[i, j] = col[j];
                }
            }
            Console.WriteLine();

            int[,] total1 = new int[,] {
                { 1,2,3,4,5},{ 2,3,4,5,6},{ 3,4,5,6,7},{ 5,6,7,8,9} };
            string formatstr = null;
            for (int i = 0; i < rowtcon.Length; i++)
            {
                for (int j = 0; j < plancon.Length; j++)
                {
                    //total[i, j] += ",";
                    formatstr += total[i, j].ToString() + ",";
                    Console.Write(total[i, j]);
                }
                formatstr = formatstr.Remove(formatstr.Length - 1);
                formatstr = formatstr + "\n";
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine(formatstr);

        }
        private static void Day04_TestStringBuilder()
        {
            string Contentstr = @"1,2,3,4,5
3,4,5,1,3
4,5,2,4,4
2,4,5,2,5";
            string[] rowtcon = Contentstr.Split("\r\n");
            string[] plancon = rowtcon[0].Split(',');
            string[,] total = new string[rowtcon.Length, plancon.Length];
            for (int i = 0; i < rowtcon.Length; i++)
            {
                string[] col = rowtcon[i].Split(',');
                for (int j = 0; j < plancon.Length; j++)
                {
                    total[i, j] = col[j];
                }
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < rowtcon.Length; i++)
            {
                for (int j = 0; j < plancon.Length; j++)
                {
                    builder.Append(total[i, j]).Append(',');
                }
                builder.Append("\r");
                builder.Remove(builder.Length - 1, 1);
            }
            Console.WriteLine(builder);
            builder.Clear();
            // Console.WriteLine(builder);
        }
        private static void Day04_TestTocharArray()
        {
            //字符串转字符数组
            string str = "你好";
            char[] getstr = str.ToCharArray();
            foreach (var item in getstr)
            {
                Console.Write(item);
            }
            Console.WriteLine();
            //字符串首尾空白字符移除
            string str1 = "     adad eewf wf fff we fwe ff    ";
            Console.WriteLine(str1.Trim());


        }
        #endregion
        #region  Class,enum,继承
        private static void Day05()
        {
            //拼接网址  
            // string mainURL = "";
            //Hashtable th = Add(1,"343");
            Hashtable ht = new Hashtable();
            ht.Add("name", "zhangsan");
            ht.Add("password", "123344");
            Console.WriteLine(Day05_GetULR("http://127.0.0.1", ht));

            //枚举类型enum
            Week week = Week.FIR;
            Console.WriteLine((int)week);

            //Test_();
            GC.Collect(); //人为垃圾回收
            //类
            Person p = new Person();
            p.Score = 60;
            Person p2 = p;
            p2.Score = 65;
            Console.WriteLine(p.Score);


            // p.SetName("张三");
            // p.SetAge(12);
            //Console.WriteLine(p.GetName());
            //继承
            Boy boy = new Boy();
            // boy.SetAge(1);
            // Console.WriteLine(boy.GetAge());

            p.Show();

            Person p1 = new Person("哈哈哈", 12);
            p1.Show();

            //属性
            p1.Name = "adffhoefi";
            Console.WriteLine(p1.Name);

            //单例模式,无论调用几次DataMar只会new一次对象
            DataMar dataMar = DataMar.Getinstance();
            DataMar dataMar1 = DataMar.Getinstance();

            var s1 = "Hi! ";
            var s2 = s1;
            s1 += "Fanguzai!";
            Console.WriteLine(s2);




        }
        private static string Day05_GetULR(string mainURl, Hashtable ht)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(mainURl).Append("?");
            foreach (DictionaryEntry item in ht)
            {
                builder.Append(item.Key).Append("=").Append(item.Value).Append("&");

            }
            builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }
        #endregion
        #region 函数重载
        //重载
        static void Func(int a, int b) { }
        static void Func(float a, float b) { }
        static void Func(int a, float b) { }
        #endregion
        #region  索引
        private static void Day06()
        {
            Person p = new Person();
            p.Score = 50;
            Person p1 = p; //声明一个对象并指向p的空间
            p1.Score = 60;
            Console.WriteLine(p.Score);  //60

            CardManager cm = new CardManager("sad");
            //访问数组，1
            // cm.card[20] = 40;
            // Console.WriteLine(cm.card[20]);
            //利用索引访问,2

            cm[10] = 20;
            Console.WriteLine(cm[10]);
            cm[1.0f] = "fdoeifheiwo";
            Console.WriteLine(cm[1.0f]);

            string[] ss = new string[10];
            CardManager cm1 = new CardManager("张飞");
            ss[0] = cm1.Name;
            cm1.Name = "张飞112";
            Console.WriteLine(ss[0]);

            UIManager win = new UIManager();
            win["login"] = "loginWindow";
            win["icon"] = "lconwindow";
            Console.WriteLine(win["login"]);

            // ?
            People p11 = new People("韩信");
            People p22 = new People("曹操");
            Console.WriteLine(p11);
            PeopleManager PM = new PeopleManager();
            PM[0] = p11;
            PM[1] = p22;

            People[] list = new People[2];

            list[0].Name = "曹操1";
            list[1].Name = "曹操2";
            // Console.WriteLine(PM[0]);

            PeopleManager.ArrayAsPeople(list);
            //Console.WriteLine(list[0]);

        }
        #endregion
        #region 运算符的重载、struct
        private static void Day07()
        {
            //正常情况下要new  ,结构体是值类型
            Student student = new Student();
            Student student1 = new Student();
            student.Age = 10;
            student1.Age = student.Age;
            student1.Age = 20;
            Console.WriteLine(student.Age);


            //访问static修饰的字段
            Console.WriteLine(Student.number);
            Console.WriteLine(student.Birthday);
            Student student2 = new Student("张三", 18);
            Console.WriteLine(student2);

            Student student3 = new Student("张三", 18);
            student3.ShowTime();
            Thread.Sleep(2000);
            //复数的重载
            Complex c = new Complex(1, 2);
            Complex c1 = new Complex(2, 3);
            Complex c2 = c + c1;
            c2.Show();
            Complex c3 = c * c1;
            c3.Show();

        }
        #endregion
        #region 异常处理、继承、多态
        private static void Day08()
        {

            /* 多态*/
            GunBase gun2 = new GunBase();
            gun2.Shoot();

            GunBase gun = new AK();
            gun.Shoot();   //运行时类型(AK)
            GunBase gun1 = new AWM();
            gun1.Shoot();

            /*异常*/
            //Day08_Test01();
            //Console.WriteLine(Day08_Test02()); 
            /* 继承*/
            //向上类型转换,实际什么类型就输出什么类型，没有虚方法的前提下
            Animal animal = new Horse("wew");
            animal.Voice();

            Horse horse = new Horse("wddf");
            horse.Voice();

            //向下类型转换，不安全
            //Horse h1 = new Animal();
            //h1.Voice();
        }
        private static void Day08_Test01()
        {
            Point point = new Point(1.227f, 2.4f);
            Console.WriteLine(point);

            Circle cirle = new Circle(point, 5, 100);
            cirle.Draw();
            //异常处理  try(把可能出现异常的代码放到try里面)-->catch-->final(不管有没有问题都处理)
            try
            {
                cirle.Age = 200;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Final");
            }
            //cirle.Age = 200; //抛出异常
            Console.WriteLine("面积：{0:N}", cirle.Area());
            Console.WriteLine("周长：{0:N}", cirle.Length());

        }
        private static Person Day08_Test02()
        {
            int a = 1;
            int b = 2;
            int n = 1;
            Person p = new Person();
            p.age = 1;
            try
            {
                int k = a / b;
                //return n;
                //嵌套函数，函数里面的异常也可以捕获
                //TestException();
                return p;
            }
            catch (DivideByZeroException di)
            {
                Console.WriteLine(di.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                Console.WriteLine("Final");
                n++;
                p.age++;
            }

        }
        static void TestException()
        {
            int a = 20;
            int b = a / 0;
        }
        #endregion
        #region 接口
        private static void Day09()
        {
            //A a = new C();
            //输入字符，用于Switch case
            // userInput();
            Fruit fruit = new Apple(23.1f);
            fruit.GrowInArea();
            Apple ap1 = new Apple(63.1f);
            Apple ap2 = new Apple(63.1f);
            if (ap1.Equals(ap2))
            {
                Console.WriteLine("Is Equals");
            }
            if (object.ReferenceEquals(ap1, ap2))
            {
                Console.WriteLine("Reference");
            }
            if (ap1 == ap2)
            {
                Console.WriteLine("==");
            }

            //接口
            InMyInterface my = new My(12);
            InMyInterface my1 = new My(12);
            Console.WriteLine(my.ComapreTo(my1));

            //显式接口
            InMyInterface m1 = new My();
            ISerialize m2 = new My();
            m1.SayHello();
            m2.SayHello();


        }
        #endregion
        #region 静态类

        private static void Day10()
        {
            CreateGun.GetCreateGun(Gun_Type.Gun_M416);
            //类名访问const成员
            Console.WriteLine(Gun.sd);





        }
        #endregion
        #region OOP练习 window
        private static void Day11()
        {
            WindowManager windowMgr = WindowManager.GetInatance();
            windowMgr.OpenWindows(Window_Type.KillWindow);
            windowMgr.OpenWindows(Window_Type.HomeWindow);
            windowMgr.CloseWindows(Window_Type.HeroWindow, true);
        }
        #endregion
        #region 值类型与引用类型练习
        class AA { }
        class BB : AA { }
        class CC : AA { }
        struct Testq
        {
            public int A { get; set; }
            Testq(int a) { this.A = a; }
        }
        private static void Day12()
        {
            //引用类型做实参
            Ref myref = new Ref();
            myref.Num = 1;

            Add(myref);
            Console.WriteLine("函数外的Num值{0}", myref.Num);
            //结构体做实参
            Testq test = new Testq();
            test.A = 10;
            Add(ref test);
            Console.WriteLine("函数外的test.A值{0}", test.A);
            //重载out或ref修饰的函数，out或ref不能同时重载一个函数
            List<Person> list = new List<Person>();
            //as
            AA a = new AA();
            BB b = a as BB;
            CC c = a as CC;
            if (b == null)
            {
                Console.WriteLine("b is Null");
            }
            if (c == null)
            {
                Console.WriteLine("c is Null");
            }
            //Typeof
            Type aq1 = typeof(string);
            Console.WriteLine("{0}", aq1);
            Console.WriteLine(typeof(string));

        }
        private static void Add(Ref myref)
        {
            myref.Num += 1;
            Console.WriteLine("函数内的Num值{0}", myref.Num);
        }
        private static void Add(ref Testq a)
        {
            a.A += 1;
            Console.WriteLine("函数内的test.A值{0}", a.A);
        }
        private static void AddSum(int a) { }
        private static void AddSum(ref int a) { }
        //private static void AddSum(out int a) { }


        #endregion
        #region 栈、队列、泛型
        private static void Day13()
        {
            //栈
            Stack stack = new Stack();
            stack.Push(344);
            stack.Push("hello");
            stack.Push("dddd");
            Console.WriteLine("Pop Before;{0}", stack.Count);
            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }
            //出栈
            Console.WriteLine("出栈后--------");
            stack.Pop();
            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }
            Stack s1 = stack.Clone() as Stack;
            Console.WriteLine("Pop After:{0}", s1.Count);
            //GC.Collect();

            //队列
            Queue queue = new Queue();
            queue.Enqueue("qwewe");
            queue.Enqueue("wwwww");
            queue.Enqueue("rrrr");
            queue.Enqueue("uuuu");
            while (queue.Count != 0)
            {
                queue.Dequeue();
            }
            //Console.WriteLine(queue.Peek());
            Console.WriteLine(queue.Count);
            //泛型
            Stopwatch stopwatch = new Stopwatch();
            ArrayList list = new ArrayList();
            stopwatch.Start();
            for (int i = 0; i < 100000; i++)
            {
                list.Add(i);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);

            Stopwatch stopwatch1 = new Stopwatch();
            List<int> list1 = new List<int>();
            stopwatch1.Start();
            for (int i = 0; i < 100000; i++)
            {
                list1.Add(i);
            }
            stopwatch1.Stop();
            Console.WriteLine(stopwatch1.Elapsed);

            //泛型类
            Stacka<int> st = new Stacka<int>();
            Console.WriteLine(st.Sum(20, 30));

            //实现比较接口的泛型类
            Console.WriteLine(Comparer<int, float>.CompareGeneric(230, 30));
            Comparer<int, float> cmp = new Comparer<int, float>();
            Console.WriteLine(cmp.Code1);
            Console.WriteLine(cmp.Code2);

            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(1, "ashdosid");
            dic.Add(2, "ASDDF");
            Console.WriteLine(dic[1]);

            //静态泛型类,每一种类型只会调用一次
            GenericClass<int>.Print();
            GenericClass<int>.Print();
            Console.WriteLine("------------");
            GenericClass<string>.Print();
            //每一种类型都有单独的静态数据
            GenericClass<int>.Code = 10;
            Console.WriteLine(GenericClass<int>.Code);
            Console.WriteLine(GenericClass<string>.Code);
            //泛型静态方法
            int res = GenericMethodClass.GetMax<int>(20, 30);
            Console.WriteLine(res);

            //继承字典
            MyDictionary<Person> p = new MyDictionary<Person>();
            Type t = typeof(MyDictionary<>);
            Console.WriteLine("是否为开放类型：" +
                t.ContainsGenericParameters);
            t = typeof(MyDictionary<int>);
            Console.WriteLine("是否为开放类型：" +
                t.ContainsGenericParameters);

            //基类约束
            Boy2<Boy> b = new Boy2<Boy>();
            //接口约束
            // WindowMove<Person> window = new WindowMove<Person>();
            //window.Move();
            //构造约束
            WindowGz<int> wind = new WindowGz<int>();

        }
        #endregion
        #region 泛型委托、匿名函数、闭包
        private static void Day14()
        {
            Delegate_ del = new Delegate_();
            SortHelper<int> sortHelper = new SortHelper<int>();
            int[] arr = new int[] { 19, 23, 3, 45, 2, 7, 67 };
            sortHelper.Sort(arr);
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
            //泛型基类的继承
            People<TreeBase> miao = new People<TreeBase>();
            miao.Plant(new SoSoTree());
            miao.Plant<SoSoTree>(new SoSoTree());
            //可空类型
            int? value = null;
            Display(value);
            //空合并操作符
            //当左边的变量不为空时，返回左边的值，否则返回右边的值
            int? inq = 12;
            int dd = inq ?? 1;
            Console.WriteLine(dd);
            //匿名函数
            VoteDel1 dell = delegate (string name)
            {
                Console.WriteLine($"投给了{name}");
            };
            dell("李四");
            //闭包、匿名函数内部使用到外面的变量
            //VoteDel1 del2 = GetVote1();
            //del2("ss");
            //del2("dd");
            AddDel del_add = delegate (int a, int b)
            {
                return a + b;
            };
            Console.WriteLine(del_add(10, 20));

        }
        delegate void VoteDel();
        delegate void VoteDel1(string name);
        delegate int AddDel(int a, int b);
        private static void Display(int? value)
        {
            Console.WriteLine("可空类型值是否有值：{0}", value.HasValue);
            if (value.HasValue)
            {
                Console.WriteLine("值为：{0}", value.Value);
            }
        }
        private static VoteDel GetVote()
        {
            int count = 1;
            VoteDel del = delegate ()
            {
                count++;
                Console.WriteLine("闭包{0}", count);
            };
            del();
            return del;
        }
        private static VoteDel GetVote1(string name)
        {
            int count = 1;
            VoteDel del = delegate ()
            {
                count++;
                Console.WriteLine($"{name}");
            };
            del();
            return del;
        }

        #endregion
        #region Var、对象集合初始化器、Lambda、迭代器
        private static void Day15()
        {
            var str = "sdfhdsoif";
            //对象初始化器
            Person p = new Person
            {
                Name = "sdsd",
                Score = 23,
                age = 12,
                Mode = "asd"
            };
            List<Person> list = new List<Person> {
            new Person{ Name = "sd",age = 12, Score = 12},
            p
            };
            //匿名对象类型
            var per = new { Name = "sdsd" };
            //匿名函数
            delLambada del1 = delegate (int a)
            {
                return a * a;
            };
            //表达式Lambda，左边输入右边输出
            delLambada del2 = x => x * x;
            del2(2);
            //语句Lambda
            delLambada de3 = x =>
            {
                Console.WriteLine("嘿嘿");
                return x * x;
            };
            de3(2);
            delLambda1 de4 = (a, b) =>
            {
                return a * b;
            };
            de4(1, 2);
            delLambda1 del5 = (a, b) =>
            {
                Console.WriteLine("sss");
                return a * b;
            };
            del5(12, 3);
            Console.WriteLine(Num());
            //泛型委托的Lambda,最后1一个是返回值，其余都是参数
            Func<int, int, int> func1 = (a, b) => a + b;
            Console.WriteLine(func1(1, 2));

            Func<int> func2 = () => 10;
            //迭代器
            Friends collects = new Friends();
            Console.WriteLine("集合数量：" + collects.Count());
            foreach (Friend f in collects)
            {
                Console.WriteLine(f.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("迭代器遍历：");
            IEnumerator itor = collects.GetEnumerator();
            while (itor.MoveNext())
            {
                Console.WriteLine(itor.Current.ToString());
            }

        }

        delegate int delLambada(int a);
        delegate int delLambda1(int a, int b);
        static int Num()
        {
            Random random = new Random();
            return random.Next(0, 7);
        }

        #endregion
        #region 扩展
        private static void Day16()
        {
            //代理
            Entiy en = new Entiy();
            DaiLi dl = new DaiLi();
            en.Set(dl);
            en.DoSomething();
            //扩展
            //第一个参数this，表示对哪个类进行扩展
            //第二个参数，才是调用时真正调用的函数
            GameObject obj = new GameObject();
            string name = "Rigidbody";
            //第一种方法
            string get = GameObjectExtern.GetOrAddComponent(obj, name);
            //第二种方法
            string get1 = obj.GetOrAddComponent(name);
            Console.WriteLine(get);
            Console.WriteLine(get1);

            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int sum = ListExtern.JSum(list);
            Console.WriteLine(sum);
            Console.WriteLine(list.JSum());

            Person1 p = new Person1();
            p.Print();
            p.Print("哈哈");

            //协变性,out做输出类型

            IDmon1<string> one = new One();
            IDmon1<object> two = new Two();
            //two = one;
            one = two;  //×
            two.Method("哈哈");

            //动态类型
            dynamic i;
            i = 1;
            Console.WriteLine($"{i.GetType()}");

            Func<int, int> s = sa => sa++;
            //Linq
            int[] number = { 1, 2, 3, 4, 4, 5, 6, 5, 6 };
            var sss = from ii in number
                      where ii % 2 == 0
                      select ii;
            foreach (var item in sss)
            {
                Console.Write(item + " ");
            }

            var dd = from jj in number
                     orderby jj ascending //升序
                     select jj;
            foreach (var item in dd)
            {
                Console.Write(item + " ");
            }
        }

        #endregion
        #region 多线程
        public static void Day17()
        {

            Console.WriteLine("主线程");
            ThreadStart threadStart = new ThreadStart(Multithreading.Worker);
            //ThreadStart threadStart = Multithreading.Worker; √

            Thread thread = new Thread(Multithreading.Worker);

            //后台线程，主线程不会等待其结束
            thread.IsBackground = true;
            thread.Start();

            thread.Join();

            //传参给子线程
            ParameterizedThreadStart fu = new ParameterizedThreadStart(Multithreading.Worker1);
            Thread thread1 = new Thread(fu);
            thread1.Name = "MMM";
            thread1.IsBackground = true;
            //thread1.Start(thread1.Name);

            /*while (true)
            {
                if (Multithreading.message.Count>0)
                {
                    Console.WriteLine(Multithreading.message.Dequeue());
                }
            }*/
            //线程池
            ThreadPool.QueueUserWorkItem(Multithreading.Worker1);
            // Thread.Sleep(1000);
            //Console.WriteLine(ThreadPool.ThreadCount);
            ThreadPool.QueueUserWorkItem(Multithreading.Worker1, "参数");
            ThreadPool.QueueUserWorkItem(Multithreading.Worker1, "参数");

            //Console.WriteLine(ThreadPool.ThreadCount);
            //死锁
            Thread thread2 = new Thread(Multithreading.SaleTicke1);
            Thread thread3 = new Thread(Multithreading.SaleTicke2);
            //thread2.Start();
            //thread3.Start();
            //回调函数
            DownFinishDel del = delegate
            {

                Console.WriteLine("下载好了哟！");

            };
            Thread thread4 = new Thread(Multithreading.DownFinishFile);
            thread4.Start(del);

            //type
            Type t = typeof(string);
            Console.WriteLine(t);
            string s = "asjkdhsdfh";
            Type t1 = Type.GetType(s);
            Console.WriteLine(t1);

            //反射
            ReflectionClass reflectionClass = new ReflectionClass();
            Type type = typeof(ReflectionClass);
            Console.WriteLine(type);
            Console.WriteLine(type.FullName);
            Console.WriteLine(type.IsEnum);
            //获取类中的构造方法
            ConstructorInfo[] constructInfos = type.GetConstructors();
            foreach (ConstructorInfo item in constructInfos)
            {
                ParameterInfo[] pas = item.GetParameters();
                foreach (var item1 in pas)
                {
                    Console.WriteLine(item1.ParameterType.ToString() + " " + item.Name);
                }
            }

            //参数数组，第一个参数是string,第二个参数是int
            Type[] pt = new Type[2];
            pt[0] = typeof(string);
            pt[1] = typeof(int);
            //根据参数类型获取构造函数，有可能是多个构造函数
            ConstructorInfo ci = type.GetConstructor(pt);
            object[] pars = new object[] { "字符串类型string", 2 };
            //使用Invoke方法调用构造函数，传递参数pars
            object coninvoke = ci.Invoke(pars);
            ((ReflectionClass)coninvoke).Show();

            //使用Activatar创建对象
            object obj5 = Activator.CreateInstance(type, "张三", 40);
            ((ReflectionClass)obj5).Show();

            MethodInfo[] methods = type.GetMethods();
            foreach (var item in methods)
            {
                ParameterInfo[] inf = item.GetParameters();
                foreach (var item1 in inf)
                {
                    Console.WriteLine(item1.Name + " " + item1.ParameterType);
                }
            }
            Type type1 = typeof(ReflectionClass);
            object obj6 = Activator.CreateInstance(type1);
            //字段
            FieldInfo[] fieldInfo = type1.GetFields();
            fieldInfo[0].SetValue(obj6, "ssss");
            fieldInfo[1].SetValue(obj6, 23);
            //属性
            PropertyInfo[] propertyInfo = type1.GetProperties();
            propertyInfo[0].SetValue(obj6, 12222);
            propertyInfo[1].SetValue(obj6, 44);
            (obj6 as ReflectionClass).Show();


        }


        #endregion
        #region 程序集
        private static void Day18()
        {
            //程序集
            Assembly assembly = Assembly.Load("ConsoleApp1");
            //获取程序集中的类型
            Type t = assembly.GetType();
            Console.WriteLine(t.FullName);
            //获取DLL文件
            Assembly assembly1 = Assembly.LoadFrom("C:\\Users\\TP\\Desktop\\MyDll.dll");
            object[] ob = new object[] { };
            object obj = assembly1.CreateInstance("MyDll.Person", true, 0, null, ob, null, null);
            Type t1 = obj.GetType();
            MethodInfo[] methods = t1.GetMethods();

        }
        #endregion
        #region 状态更换
        private static void Day19() {
            Context con = new Context(new AttackA());
            con.Request();
            con.Request();
            //中介
            ConcreateMediator mediator = new ConcreateMediator();
            ConcreateColleague1 concreateColleague1 = new ConcreateColleague1(mediator);
            ConcreateColleague2 concreateColleague2 = new ConcreateColleague2(mediator);
            mediator.Colleague1 = concreateColleague1;
            mediator.Colleague2 = concreateColleague2;

            mediator.Send("你好哇", concreateColleague1);


        }
        #endregion
    }

}
