<h2>Tower Defense Game</h3>

<h3>Welcome to Tower Defense Game</h3><hr>

![image](https://github.com/user-attachments/assets/478427f8-2c9e-4b58-8f25-6c6d0a5cfc71)

This project covers the design of a strategic tower defense game where one needs to protect the kingdom from waves of cursed monsters. In this game, there is a need to build towers, manage resources, and control the main character-the hero-with the only aim of stopping the monsters from reaching the core of the kingdom. We used Unity for the development platform, and as such, C# became our programming language. In this way, we were able to create a game working on both web and mobile devices. 

<h3>Level Structure</h3>
The game includes two primary levels, each escalating in difficulty and enemy complexity. The design favors a linear path from left to right, where enemies spawn on the left side and progress toward the kingdom on the right. This structure simplifies the gameplay while emphasizing tower defense strategy, allowing players to anticipate enemy paths and plan tower placements effectively.
<h5>Level 1</h5>

![image](https://github.com/user-attachments/assets/b2f5a5b9-5036-4700-985f-6cea763d7d25)

<h5>Level 2 </h5>

![image](https://github.com/user-attachments/assets/dae87e8e-a8b5-496b-82d7-a5027972bf33)

<h3>Game Characters</h3>
<h4>Hero</h4>
The hero is the kingdom’s last line of defense, stationed on the right side, tasked with stopping any monster that evades the towers and reaches the core.

<h4>Enemies</h4>
The cursed monsters are the primary antagonists, advancing in waves with increasing difficulty. Each enemy type has unique attributes and abilities that add complexity to the gameplay.

<h3>Tower Types and Strategic Depth</h3>
The game features three tower types, each designed with unique attributes and upgrade paths that add depth to the gameplay. Selecting the right towers and upgrading them strategically are essential to surviving tougher waves.
<ol>
  <li>
    Gun Tower:
    <ul>
      <li>Function: Basic ranged attack tower with moderate damage and firing rate.</li>
      <li>Cost: 100 coins to install, 150 coins to upgrade.</li>
      <li>Damage: 30 for basic, 50 for upgraded</li>
      <li>Strategic Use: Effective against early, weaker enemies. With upgrades, it becomes a versatile tower that can be used in choke points or alongside other towers for a balanced defense.</li>
    </ul>
  </li>
  <li>Slow Tower:
    <ul>
      <li>Function: Slows down enemies within its radius, giving other towers and the hero more time to inflict damage.</li>
      <li>Cost: 200 coins to install, 300 coins to upgrade.</li>
      <li>Damage: Reduces enemy speed multiplied by 0.5, 3 seconds for basic and 5 seconds for upgraded.</li>
      <li>Strategic Use: Best used near the beginning of the enemy path or at key choke points to delay fast-moving enemies. It’s particularly useful in mid-level waves where enemy speed increases, allowing other towers to target grouped enemies more effectively.</li>
    </ul>
  </li>
  <li>
    Laser Tower:
    <ul>
      <li>Function: High-damage, single-target tower with a slower firing rate, ideal for dealing with bosses or high-HP enemies.</li>
      <li>Cost: 300 coins to install, 450 coins to upgrade.</li>
      <li>Damage: 0.2 for basic, 0.5 for upgraded</li>
      <li>Strategic Use: Best placed near the end of the path to deliver strong final blows to enemies that survive the initial defenses. Its high damage output is essential for handling the tougher enemies and bosses in the final waves.</li>
    </ul>
  </li>
</ol>

<h3>Hero Mechanics and Role in Strategy</h3>
The hero acts as a mobile defender on the right side, engaging with enemies that manage to evade or survive tower attacks. The hero’s abilities add an interactive layer to the strategy, as players must manage the hero’s position and health to maximize effectiveness.
<ul>
  <li>Movement: The hero can move along the endpoint area, providing flexibility in intercepting enemies at different parts of the final defense line.</li>
  <li>Attack Mechanism: The hero’s primary attack is a melee strike with a sword, effective for close-range engagement. This allows the hero to handle any stragglers or tougher enemies that get past the towers.</li>
  <li>Life Management: The hero has 20 lives and can complete the entire level. Between levels, the hero’s lives will not be restored.</li>
  <li>The hero’s role complements the towers, providing an additional layer of defense and flexibility, especially in cases where enemy types vary in speed or strength.</li>
</ul>

<h3>License</h3>
All 3rd-party assets and libraries used in this project retain all rights under their respective licenses.

