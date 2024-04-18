using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TestTools;




public class TopDownControllerTests
{
   [UnityTest]
    public IEnumerator DirectionNormalizationTest()
    {
        var gameObject = new GameObject();
        var topDownController = gameObject.AddComponent<TopDownController>();
        var spriteSheets = gameObject.AddComponent<SpriteSheets>();

        topDownController.Direction = new Vector2(2, 2);
        yield return null; // Wait for the next frame update

        float tolerance = 0.01f; // Adjust this tolerance as needed
        Assert.LessOrEqual(Vector2.Distance(new Vector2(0.71f, 0.71f), topDownController.Direction), tolerance, "Direction should be normalized");
    }
    [Test]
    public void GetSpriteNorthDirectionTest()
    {
        var gameObject = new GameObject();
        var topDownController = gameObject.AddComponent<TopDownController>();
        var spriteSheets = gameObject.AddComponent<SpriteSheets>();

        
        topDownController.Direction = new Vector2(0, 1);
        var sprites = spriteSheets.GetSpriteDirection(topDownController.Direction,UnityEngine.Analytics.Gender.Male);
        Assert.AreEqual(spriteSheets.nMaleSprites, sprites, "Should return north sprites");

        // Add similar tests for other directions
    }
    [Test]
    public void GetSpriteNorthEastDirectionTest()
    {
        var gameObject = new GameObject();
        var topDownController = gameObject.AddComponent<TopDownController>();
        var spriteSheets = gameObject.AddComponent<SpriteSheets>();


        
        topDownController.Direction = new Vector2(1, 1);
        var sprites = spriteSheets.GetSpriteDirection(topDownController.Direction,UnityEngine.Analytics.Gender.Male);
        Assert.AreEqual(spriteSheets.neMaleSprites, sprites, "Should return north east sprites");

        // Add similar tests for other directions
    }
    [Test]
    public void GetSpriteEastDirectionTest()
    {
        var gameObject = new GameObject();
        var topDownController = gameObject.AddComponent<TopDownController>();
        var spriteSheets = gameObject.AddComponent<SpriteSheets>();


        
        topDownController.Direction = new Vector2(1, 0);
        var sprites = spriteSheets.GetSpriteDirection(topDownController.Direction,UnityEngine.Analytics.Gender.Male);
        Assert.AreEqual(spriteSheets.eMaleSprites, sprites, "Should return east sprites");


        // Add similar tests for other directions
    }
    [Test]
    public void GetSpriteSouthEastDirectionTest()
    {
        var gameObject = new GameObject();
        var topDownController = gameObject.AddComponent<TopDownController>();
        var spriteSheets = gameObject.AddComponent<SpriteSheets>();


        
        topDownController.Direction = new Vector2(1,-1);
        var sprites = spriteSheets.GetSpriteDirection(topDownController.Direction,UnityEngine.Analytics.Gender.Male);
        Assert.AreEqual(spriteSheets.seMaleSprites, sprites, "Should return south east sprites");

        // Add similar tests for other directions
    }
    [Test]
    public void GetSpriteSouthDirectionTest()
    {
        var gameObject = new GameObject();
        var topDownController = gameObject.AddComponent<TopDownController>();
        var spriteSheets = gameObject.AddComponent<SpriteSheets>();


        
        topDownController.Direction = new Vector2(0, -1);
         var sprites = spriteSheets.GetSpriteDirection(topDownController.Direction,UnityEngine.Analytics.Gender.Male);
        Assert.AreEqual(spriteSheets.nMaleSprites, sprites, "Should return north sprites");

       
    }

    [Test]
    public void SetSpriteTest()
    {
        var gameObject = new GameObject();
        var topDownController = gameObject.AddComponent<TopDownController>();

        
        topDownController.spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

        
    }
}

