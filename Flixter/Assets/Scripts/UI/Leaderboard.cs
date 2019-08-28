using System;
using System.Net;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour {
	//TODO: read it from xml
	readonly string[] names = { "Aspect", "Kraken", "Bender", "Lynch", "Big Papa", "Mad Dog", "Bowser", "O'Doyle", "Bruise", "Psycho", "Cannon", "Ranger", "Clink", "Ratchet", "Cobra", "Reaper", "Colt", "Rigs", "Crank", "Ripley", "Creep", "Roadkill", "Daemon", "Ronin", "Decay", "Rubble", "Diablo", "Sasquatch", "Doom", "Scar", "Dracula", "Shiver", "Dragon", "Skinner", "Fender", "Skull Crusher", "Fester", "Slasher", "Fisheye", "Steelshot", "Flack", "Surge", "Gargoyle", "Sythe", "Grave", "Trip", "Gunner", "Trooper", "Hash", "Tweek", "Hashtag", "Vein", "Indominus", "Void", "Ironclad", "Wardon", "Killer", "Wraith", "Knuckles", "Zero", "Steel", "Kevlar", "Lightning", "Tito", "Bullet-Proof", "Fire-Bred", "Titanium", "Hurricane", "Ironsides", "Iron-Cut", "Tempest", "Iron Heart", "Steel Forge", "Pursuit", "Steel Foil", "Upsurge", "Uprising", "Overthrow", "Breaker", "Sabotage", "Dissent", "Subversion", "Rebellion", "Insurgent", "Loch", "Golem", "Wendigo", "Rex", "Hydra", "Behemoth", "Balrog", "Manticore", "Gorgon", "Basilisk", "Minotaur", "Leviathan", "Cerberus", "Mothman", "Sylla", "Charybdis", "Orthros", "Baal", "Cyclops", "Satyr", "Azrael", "Ballistic", "Furor", "Uproar", "Fury", "Ire", "Demented", "Wrath", "Madness", "Schizo", "Rage", "Savage", "Manic", "Frenzy", "Mania", "Derange", "V", "Atilla", "Darko", "Terminator", "Conqueror", "Mad Max", "Siddhartha", "Suleiman", "Billy the Butcher", "Thor", "Napoleon", "Maximus", "Khan", "Geronimo", "Leon", "Leonidas", "Dutch", "Cyrus", "Hannibal", "Dux", "Mr. Blonde", "Agrippa", "Jesse James", "Matrix", "Bleed", "X-Skull", "Gut", "Nail", "Jawbone", "Socket", "Fist", "Skeleton", "Footslam", "Tooth", "Craniax", "Head-Knocker", "K-9", "Bone", "Razor", "Kneecap", "Cut", "Slaughter", "Soleus", "Gash", "Scalp", "Blood", "Scab", "Torque", "Wracker", "Annihilator", "Finisher", "Wrecker", "Destroyer", "Overtaker", "Clencher", "Stabber", "Saboteur", "Masher", "Hitter", "Rebel", "Crusher", "Obliterator", "Eliminator", "Slammer", "Exterminator", "Hell-Raiser", "Thrasher", "Ruiner", "Mutant", "Torpedo", "Wildcat", "Automatic", "Cannon", "Hellcat", "Glock", "Mortar", "Tomcat", "Sniper", "Siege", "Panther", "Carbine", "Bullet", "Jaguar", "Javelin", "Aero", "Bomber", "Howitzer", "Albatross", "Strike Eagle", "Gatling", "Arsenal", "Rimfire", "Avenger", "Hornet", "Centerfire", "Hazzard", "Demolition", "Power Train", "Yarder", "Chainsaw", "Excavator", "Trencher", "Wrench", "Shovel", "Pile Driver", "Terror", "Demise", "Phantom", "Freak", "Grim", "Sepulcher", "Axe", "Menace", "Damned", "Axe-man", "Dementor", "Kafka", "Executioner", "Nightshade", "Phantasm", "Hollowman", "Venom", "Scream", "Garrot", "The Unholy", "Shriek", "Abyss", "Rot", "Wraith", "Chasm", "Omen", "Bodybag", "Ghoul", "Midnight", "Morgue", "Mace", "Falchion", "Montante", "Battleaxe", "Zweihander", "Hatchet", "Billhook", "Club", "Hammer", "Caltrop", "Maul", "Sledgehammer", "Longbow", "Bludgeon", "Harpoon", "Crossbow", "Lance", "Angon", "Pike", "Tiger Claw", "Fire Lance", "Poleaxe", "Brass Knuckle", "Matchlock", "Quarterstaff", "Gauntlet", "Bullwhip", "War Hammer", "Katar", "Flying Claw", "Spear", "Dagger", "Slungshot", "Katana", "Gladius", "Aspis", "Saber", "Cutlass", "Blade", "Broadsword", "Scimitar", "Lockback", "Claymore", "Espada", "Machete", "Grizzly", "Wolverine", "Deathstalker", "Snake", "Wolf", "Scorpion", "Vulture", "Claw", "Boomslang", "Falcon", "Fang", "Viper", "Ram", "Grip", "Sting", "Boar", "Black Mamba", "Lash", "Tusk", "Goshawk", "Gnaw", "Amazon", "Majesty", "Anomoly", "Malice", "Banshee", "Mannequin", "Belladonna", "Minx", "Beretta", "Mirage", "Black Beauty", "Nightmare", "Calypso", "Nova", "Carbon", "Pumps", "Cascade", "Raven", "Colada", "Resin", "Cosma", "Riveter", "Cougar", "Rogue", "Countess", "Roulette", "Enchantress", "Shadow", "Enigma", "Siren", "Femme Fatale", "Stiletto", "Firecracker", "Tattoo", "Geisha", "T-Back", "Goddess", "Temperance", "Half Pint", "Tequila", "Harlem", "Terror", "Heroin", "Thunderbird", "Infinity", "Ultra", "Insomnia", "Vanity", "Ivy", "Velvet", "Legacy", "Vixen", "Lithium", "Voodoo", "Lolita", "Wicked", "Lotus", "Widow", "Mademoiselle", "Xenon", "Kahina", "Teuta", "Isis", "Dihya", "Artemis", "Nefertiti", "Running Eagle", "Atalanta", "Sekhmet", "Colestah", "Athena", "Ishtar", "Calamity Jane", "Enyo", "Ashtart", "Pearl Heart", "Bellona", "Juno", "Belle Starr", "White Tights", "Tanit", "Hua Mulan", "Shieldmaiden", "Devi", "Boudica", "Valkyrie", "Selkie", "Medb", "Cleo", "Venus", "High and Mighty Titles", "Madam", "Empress", "Marquess", "Duchess", "Baroness", "Herzogin", "Fate", "Beguile", "Deviant", "Illusion", "Crafty", "Variance", "Delusion", "Deceit", "Caprice", "Deception", "Waylay", "Aberr", "Myth", "Ambush", "Variant", "Daydream", "Feint", "Hero", "Night Terror", "Catch-22", "Villain", "Figment", "Puzzler", "Daredevil", "Virtual", "Curio", "Mercenary", "Chicanery", "Prodigy", "Voyager", "Trick", "Breach", "Wanderer", "Vile", "Miss Fortune", "Audacity", "Horror", "Vex", "Swagger", "Dismay", "Grudge", "Nerve", "Phobia", "Enmity", "Egomania", "Fright", "Animus", "Scheme", "Panic", "Hostility", "Paramour", "Agony", "Rancor", "X-hibit", "Inferno", "Malevolence", "Charade", "Blaze", "Poison", "Hauteur", "Crucible", "Spite", "Vainglory", "Haunter", "Spitefulness", "Narcissus", "Bane", "Venom", "Brass", "Camden", "Baltimore", "Crown Heights", "Detroit", "L.A.", "Dirty Dirty", "McKinley", "NYC", "ATL", "Fiend", "Spirit", "Spellbinder", "Goblin", "Kelpie", "Jezebel", "Oracle", "Vamp", "Sorceress", "Soul", "Temptress", "She-Devil", "Revenant", "Diviner", "Hellcat", "Poltergeist", "Exorcist", "She-Wolf", "Zombie", "Seer", "Madcap", "Armor", "Blaser", "Savage", "Benelli", "Glock", "Seraphim", "Remington", "Ruger", "Winchester", "Aeon", "Tank", "Hawkeye", "Kiddo", "Torchy", "Medusa", "Buffy", "Trinity", "Irons", "Coffy", "Zoe", "Storm", "Eowyn", "Zen", "Jubilee", "Croft", "Alyx", "Dazzler", "Leeloo", "Katniss", "Aeryn", "Mathilda", "Linh", "Arya", "Padme", "Polgara", "Ygritte", "Ramona", "Elektra", "Bayonetta", "Silk Spectre", "Catwoman", "Sindel", "Helium", "Mercury", "Entropy", "Beryllium", "Radon", "Radioactive", "Neon", "Radium", "Radiate", "Phosphorus", "Element", "Ion", "Phosphorescent", "Elemental", "Eon", "Illumine", "Lab Rat", "Photon", "Chromium", "Acid", "Redox", "Arsenic", "Atom", "Redux", "Zinc", "Electron", "Hot Salt", "Selenium", "Atomic", "Vapor", "Xenon", "Nuclear", "Volt", "Osmium", "Earth Metal", "X-Ray", "Fox", "Nightshade", "Immortal", "Coyote", "Mercury", "Meteor", "Crow", "Cyanide", "Comet", "Spider", "Toxin", "Protostar", "Bat", "Virus", "Medium", "Serpent", "Arsenic", "Dark Matter", "Satyr", "Orb", "Hypernova", "Oleander", "Astor", "Stratosphere", "Hemlock", "Mortal", "Supernova", "Vegas", "Snake Eyes", "Militant", "Blackjack", "High Roller", "Lock", "Baccarat", "Pocket Rocket", "The Money", "Red Dog", "Mojave", "Dice", "Poker", "Bellagio", "Rocks", "Keno", "Nevada", "Watchface", "The House", "Reno", "Big Time", "Bookie", "Steppe", "Two Face", "Arbitrage", "Fishnet", "Deep Pockets", "Double Double", "Dagger", "Real Deal", "Explosive", "Grenade", "Pyro", "Black Cat", "Roman Candle", "Gunpowder", "M-80", "Babylon Candle", "Mortar", "Smoke Bomb", "Missile", "Firebringer", "Amaretto", "Dom", "Noir", "Bacardi", "Don", "Scotch", "Cognac", "Joose", "Whiskey", "Forever", "Revolution", "Berserk", "Unstoppable", "Sever", "Bitten", "Eternity", "Die-hard", "Corybantic", "Fanatic", "Zealot", "Crazed", "X-Treme", "Alien", "Delirious", "Rabid", "Predator", "Agitator", "Radical", "Barbarian", "Maniac", "D Waffle", "Hightower", "Papa Smurf", "57 Pixels", "Hog Butcher", "Pepper Legs", "101", "Houston", "Pinball Wizard", "Accidental Genius", "Hyper", "Pluto", "Alpha", "Jester", "Pogue", "Airport Hobo", "Jigsaw", "Prometheus", "Bearded Angler", "Joker's Grin", "Psycho Thinker", "Beetle King", "Judge", "Pusher", "Bitmap", "Junkyard Dog", "Riff Raff", "Blister", "K-9", "Roadblock", "Bowie", "Keystone", "Rooster", "Bowler", "Kickstart", "Sandbox", "Breadmaker", "Kill Switch", "Scrapper", "Broomspun", "Kingfisher", "Screwtape", "Buckshot", "Kitchen", "Sexual Chocolate", "Bugger", "Knuckles", "Shadow Chaser", "Cabbie", "Lady Killer", "Sherwood Gladiator", "Candy Butcher", "Liquid Science", "Shooter", "Capital F", "Little Cobra", "Sidewalk Enforcer", "Captain Peroxide", "Little General", "Skull Crusher", "Celtic Charger", "Lord Nikon", "Sky Bully", "Cereal Killer", "Lord Pistachio", "Slow Trot", "Chicago Blackout", "Mad Irishman", "Snake Eyes", "Chocolate Thunder", "Mad Jack", "Snow Hound", "Chuckles", "Mad Rascal", "Sofa King", "Commando", "Manimal", "Speedwell", "Cool Whip", "Marbles", "Spider Fuji", "Cosmo", "Married Man", "Springheel Jack", "Crash Override", "Marshmallow", "Squatch", "Crash Test", "Mental", "Stacker of Wheat", "Crazy Eights", "Mercury Reborn", "Sugar Man", "Criss Cross", "Midas", "Suicide Jockey", "Cross Thread", "Midnight Rambler", "Swampmasher", "Cujo", "Midnight Rider", "Swerve", "Dancing Madman", "Mindless Bobcat", "Tacklebox", "Dangle", "Mr. 44", "Take Away", "Dark Horse", "Mr. Fabulous", "Tan Stallion", "Day Hawk", "Mr. Gadget", "The China Wall", "Desert Haze", "Mr. Lucky", "The Dude", "Digger", "Mr. Peppermint", "The Flying Mouse", "Disco Thunder", "Mr. Spy", "The Happy Jock", "Disco Potato", "Mr. Thanksgiving", "The Howling Swede", "Dr. Cocktail", "Mr. Wholesome", "Thrasher", "Dredd", "Mud Pie Man", "Toe", "Dropkick", "Mule Skinner", "Toolmaker", "Drop Stone", "Murmur", "Tough Nut", "Drugstore Cowboy", "Nacho", "Trip", "Easy Sweep", "Natural Mess", "Troubadour", "Electric Player", "Necromancer", "Turnip King", "Esquire", "Neophyte Believer", "Twitch", "Fast Draw", "Nessie", "Vagabond Warrior", "Flakes", "New Cycle", "Voluntary", "Flint", "Vortex", "Freak", "Nightmare King", "Washer", "Gas Man", "Night Train", "Waylay Dave", "Glyph", "Old Man Winter", "Wheels", "Grave Digger", "Old Orange Eyes", "Wooden Man", "Guillotine", "Old Regret", "Woo Woo", "Gunhawk", "Onion King", "Yellow Menace", "High Kingdom Warrior", "Osprey", "Zero Charisma", "Highlander Monk", "Overrun", "Zesty Dragon", "Zod", "Mammoth 2", "Blinker", "FearLeSS", "Slug-em-dog", "RawSkills", "Danqqqqq", "3P-own", "VileHero", "Predator", "Freaky Ratbuster", "NeoGermal", "FireBrang", "Fatsy Bear", "HolyCombo", "ThickSKN", "Dark Matter", "BuffFreak", "HOV", "2nd Hand Joe", "ThermalMode", "Flotsams54", "Redneck Giorgio", "CodeExia", "Roadspike", "Mechani-Man", "Kazami of Truth", "Gbbledgoodk", "High Beam", "Eye Devil", "Swing Setter", "Tea Kettle", "MrOnsTr", "Wrangler Jim", "Flint Cast-Iron", "Kinged", "Lucifurious", "Lewd Dice", "RZR", "LerveDr", "Flyswat Briggs", "Legacy", "Shade Nightman", "PP Dubs", "Prone", "Hemingway Mirmillone", "Scooby Did", "Stealth", "Slinger", "Preach Man", "Unseen", "Crossing Guard", "Bad Bond", "Force", "FRMhndshk", "Easy Mac", "Sky", "SkyGod", "Toxic-oxide", "Silent", "GiddeeUP", "Irish Dze", "Apex", "DragonBlood", "Tse Tse Guy", "Shay", "IceDog", "Dallas Foxface", "Sloth", "Lounge Master", "Sprinkle Lovenuts", "Sokol", "DeathDancer", "Zorkle Sporkle", "Skool", "Pompeii Unicorn", "Noise Toy", "Flash", "Achilles Mountain", "Whip Chu", "Elektrik", "Bad Badminton", "Sly Silvermoon", "LocKz", "THRESHmSTR", "Tin Mutt", "ReiGnZ", "High-Fructose", "Sweet Bacon", "Coldy", "Sepukku", "Crazy Rox", "Beo", "Valley Guardian", "1st Degree", "Ice", "Sw00sh", "Bom Crossed", "Unleashed", "Ba1t", "Sick Saurus", "Corny", "SneakerKid", "Mad Viral", "Steel", "ShadowFax", "Clang Glyph", "Ex0tic", "Hermopolis", "xFRST", "VPR", "ManManMan", "Mosquit-No", "LyRz", "Firedog", "ELLerG!c", "Lime", "German Coach", "Hex Panther", "Energy", "Y0dler", "xSTORMx", "Blade", "WeldMaster", "Die Slice", "Tunez", "Steel Cut Toe", "Free Ham", "Truth", "Forger", "Dr. Jam Man", "Lskeee", "Black Walnut", "Seattle Jay", "Pexxious", "Journeyman", "RDTN", "Venious", "Plegasus", "Whip 2T", "Grotas", "Carrot Joker", "Skirble", "Sherm", "Switch", "Solitaire", "Gro", "Hobo Samurai", "Prof. Smirk", "Indestructible Potato", "Good William", "GuTzd", "Kamikaze Grandma", "Infinite Hole", "Are Ess Tee", "Badger the Burglar", "Sw33per", "Sir Squire", "Mauve Cactus", "Hidden Tree", "Deano", "Bruh", "AxelRoad", "Uncle Buddy", "Fadey", "Goldman", "Copilot", "Z-Boy", "Fl00d", "Bones", "DZE", "Danger Menace", "Vermilion", "Muzish", "Hang 11", "TrinitySoul", "Cooger", "Delicious Wing", "BlackExcalibur", "Kazmii", "Doz", "Risen", "AirportHobo XD", "Prez Dog", "ShadowDancer", "Cumulo", "Baked ZD", "Con Mammoth", "D-Hog-Day", "Pinball Esq", "CommandX", "Houston Rocket", "Third Moon", "Stallion Patton", "Hyper Kong", "Elder Pogue", "Rando Tank", "JesterZilla", "Lord Theus", "Omega Sub", "JigKraken", "Uncle Psycho", "Station WMD", "FrankenGrin", "Sir Shove", "Goatee Shield", "The Final Judgement", "Trash Master", "Bug Blitz", "Landfill Max", "Knight Light", "Bit Sentinel", "K-Tin Man", "Father Abbot", "Blistered Outlaw", "Scare Stone", "Admiral Tot", "RedFeet", "Chew Chew", "Scrapple", "Renegade Slugger", "Solo Kill", "Prof. Screw", "ManMaker", "RedFisher", "Trick Baron", "General Broomdog", "Automatic Slicer", "Shadow Bishop", "Raid Bucker", "Fist Wizard", "Centurion Sherman", "Atomic Blastoid", "Doz Killer", "Don Stab", "Bootleg Taximan", "Liquid Death", "Earl of Arms", "FlyGuardX", "Baby Brown", "Gov Skull", "Guncap Slingbad", "General Finish", "Sky Herald", "Pistol Hydro", "Mt. Indiana", "Lope Lope", "Greek Rifle", "Rocky Highway", "Seal Snake", "Poptart AK47", "Mad Robin", "Snow Pharaoh", "The Shield Toronto", "Jack Cassidy", "Saint La-Z-Boy", "Twix Bond", "Mad Kid", "Sultan of Speed", "Bazooka Har-de-har", "DanimalDaze", "Wolf Tribune", "Demand Chopper", "Super Flick", "B@d B0y", "Cool Law Topping", "Taz Ringer", "Frosty Squid", "Universe Bullet", "Mallow Man", "Coma Stalk", "Crash Enforcer", "Mental Prophet", "Scratch Man", "Howitzer Rise", "Iron Jesus", "Suicide Crusher", "Eight Patrol", "GoldTouch", "Troublemasher", "CosPlatoon", "Midnight Bat", "Viceswerve", "Saber-RED", "Lincoln Rider", "BoomerBox", "AlertXis", "Canine Hannibal", "Grabber", "Dance Cannon", "Darth 44", "Stud Buster", "Amphibi-Dangerous", "Mr. Alien", "Brick Mooch", "DARK HQ", "Sir Shark", "Hella Fella", "Armed Hawk", "Lucky Martian", "BuzzMouse", "Napoleonic Haze", "Tabasco Dracula", "BuzzBait", "Patton Digger", "Red Hot Kevorkian", "The Belgian", "Thunder Tank", "CZR", "DreadSherX", "Potato Sub", "JK Friend", "SubWoof330", "Dr. WMD", "Mud Finger", "Tornado Maker", "Marine Dre", "Mule Lock", "Thunder Nut", "Round Kick Boomer", "Shwatson", "Lightening Trip", "Stone Boomstick", "Grinch Cheese", "Storm Master", "Cowboy Booter", "Popeye Wipeout", "King Bass", "Insane SweepKick", "NecroBull", "Gr8 Flick", "Duke Electro", "Daffy Neo", "West Warrior", "Doughboy, Esq", "Nessie Pork", "East Army", "Fast FLAK", "New Magoo", "Rusty Vortex", "Slint FUBAR", "Master Jetson", "Sun Washer", "Hoover Spark", "King Panther", "Sly Bible", "Pyscho Hun", "Gumby Train", "Atlantic Rim", "Sapiens", "Winter Underdog", "Wooden German", "Genghis Glyph", "Sylvester Eye", "High Woo", "Grave Scuttlebutt", "Old Felix", "Low Menace", "Guillotine Trigger", "Jungle King", "Zero Corn", "Trigger Warming", "Count Eagle", "Mustard Centaur", "High Deck", "Cardinal Rebel", "Twiddle Twix", "Upper D3ckR", "Senior Smurf", "Spicy Thunder", "HighBomber", "Red Pepper", "Foot-long Fry", "360", "Sleepwalker", "Mother Hen", "Hairpin", "Clover Dragon", "Teeder", "Queen Bee", "Ladysmith", "Drift", "42nd Street", "Snapdragon", "Mother Night", "Heartbreaker", "Coffee", "The Beekeeper", "Racy Lady", "Lightweight", "Duchess", "Abiss", "Sneaky Lady", "Moxie", "Heavenly Connection", "Contrary Mary", "Tomcat", "Raggedy Ann", "Little Drunk Girl", "Eerie", "Acid Queen", "Soiled Dove", "Necessary Momentum", "Hemlock", "Cool Whip", "Toy Town", "Roller Girl", "Loot", "Easy Street", "Alley Cat", "Solitaire", "New York Mood", "Highway", "Cream", "Trash", "Romance Princess", "Low Voltage", "Emerald Goddess", "All Natural", "Spellbinder", "Nibbler", "Hoboken Nightingale", "Crumb Cake", "Treasure Devil", "Rook", "Mafia Princess", "Essex", "Backstreet", "Spitfire", "Noisy Girl", "Homerun Diva", "Curio", "Trixie", "Rope", "Magenta", "Eye Candy Kitten", "Barbwire", "Spoiler", "Nola", "Impulse", "Dahlia", "Troubled Chick", "Runway Darling", "Manly", "Feline Devil", "Bleach", "Spooky Electric", "Nutmeg", "Indigo Red", "Dandelion", "Tweety", "Santa's Little Helper", "Marshmallow Treat", "Feral Filly", "Bleachers", "Spunky Chick", "Oblivion", "Innocent Ghost", "Darkside Hooker", "Twinkle", "Saturn Extreme", "Metal Lady", "Find It Girl", "Bleeker", "Star Jammer", "Opulent Gamer", "Instant Star", "Delicious", "Undergrad", "Sassy Muffin", "Microwave", "Firecracker", "Blink", "Star Killer", "Palomino", "Intimidating Presence", "Digital Goddess", "Video Game Heroine", "Scratch", "Mirage", "Firefly", "Bliss", "Steel Heart", "Peanut Butter Woman", "Iron Butterfly", "Delirium", "Vixen", "Scuffs", "Miss Fix It", "Flashpoint", "Boost", "Stickers", "Pepper", "Jade Fox", "Demo", "Voodoo Queen", "Serendipity", "Miss Lucky", "Freesia", "Burn", "Stick Shift", "Petite Beauty", "Jersey", "Demolition Queen", "Whipsaw", "Shady Lady", "Miss Murder", "Frenzy", "Call Back Queen", "Succubus In Training", "Pink Nightmare", "Kabuki", "Despair", "Whistler", "Shamrock", "Miss Mustard", "Frosty", "Campfire Mama", "Sugar Hiccup", "Pinup Diva", "Kamikaze Granny", "Devine Melon", "White Swan", "Shivers", "Moon Cricket", "Fuzzy Logic Hottie", "Canary Apple Red", "Sun Runner", "Pitfall", "Kimono Goddess", "Dewdrop Doll", "Wiccan Trouble", "Show Off", "Moonflower", "Gentle Avenger", "Chameleon", "Sweetness", "Pixie", "Ladybird", "Dez", "Wildcat Talent", "Silver Cup", "Moon Orchid", "Golden Cougar", "Chapstick", "Swedish Pixie", "Pockets", "Lady Fantastic", "Domino", "Wild Hair", "Skylark", "Morbid Angel", "Gothic Slacker", "Charms", "Tall Sally", "Proper", "Lady In Red", "Dora the Destroyer", "Winded On Friday", "Sleek Assassin", "Most Wanted", "Granola", "Classy Dancer", "Tangerine", "Purity", "Lady Pomegranate", "Dream Killer", "Wings", "Woodland Beauty", "180", "Uluru Walker", "Hen Skittle", "Bad Beret", "Clover Rabbit", "TeederSmartie", "Mantis Queen", "Evil Smith", "BearDrift", "21st Street", "White Dragon", "NightWonka", "ManBreaker", "Cinder Coffee", "Pop Bee", "Racy Babe", "Ella of Light", "Taffy Duchess", "Abyss Tamer", "Lady Katniss", "Pixy Mox", "Heaven Sent", "Scarlet Mary", "9Lives", "DollFaceKillah", "Little Granger", "Double Eerie", "Acetic Princess", "Princess Dove", "Jelly Momentum", "Po1son", "Bridge Whip", "Toy Peep", "Rink Ruler", "Poppin Loot", "Street Jolly", "NightDream", "Dorothy Solitaire", "New York Sixlet", "Freeway", "Juno Cream", "Trash Pocky", "Bad Princess", "Mrs. Voltage", "Emerald Vine", "M8deUp", "Baby Spell", "Burst Nibbler", "Phoenix Sparrow", "ZenaCake", "Minty Devil", "Fire Queen", "Mafia Rapunzel", "Twix Esses", "Alley Fiend", "Spit Turanga", "NoiseCake", "Knock Out Star", "Killer Curio", "Trixie Doodle", "Twine X", "Bat Magenta", "Thumb Candy", "RZRWRE", "Spoiler Betty", "Apple Nola", "SprkR", "Gold Dahlia", "Troubled Pie", "Bad Beh8vior", "Manly Reno", "Devil Bread", "Chlorine", "Jo Jo Spooky", "Biscuit Meg", "Black Hole Necromancer", "Gabriel Dandelion", "Tweety Bun Bun", "Blood Taker", "Ozzie Treat", "Feral Cookie", "Ember Master", "Spunky Sphinx", "Pecan Oblivion", "Nice Gnome", "Darkside Isis", "Twinkle Cocoa", "VenusXX", "Metal Aphrodite", "Girl Brownie", "MeeP", "Athena Star", "Gamer Bean", "RightN0w2", "Delicious Cupid", "Undergrad Split", "Fire Sass", "Microwave Chardonnay", "Short Firecracker", "Engine Eye", "Killer Merlot", "Palomino Cake", "Intimidation Station", "Digital Moonshine", "Red Heroine", "Freeze Queen", "Bourbon Mirage", "Firefly Caramel", "Saturnalia", "Steel Ginger", "Gingersnap Woman", "Titanium Ladybug", "Soda Delirium", "S’more Vixen", "Cuff Queen", "Miss Rum Punch", "Flash Protein", "Boost Princess", "Rummy Stickers", "Fresh Peper", "Tin Fox", "Demo Tequila", "Tart Voodoo", "Spontan8ty", "Lucky Brandy", "Twisty Freesia", "Dark Burn", "Barbera Shift", "Juice Petite", "Alberta", "Queen Ginger", "Combo Saw", "Shadow Gal", "Murder Cherry", "Hitch Frenzy", "Referee", "Berry Succubus", "Pink C", "Noh Noh", "Blue Despair", "Snapple Whistler", "Shimmy Shammy", "Black Mustard", "Frosty Snazz", "Lumberyard", "Sugar Apple", "Aqua Diva", "Lil Rebel Ma", "Divine Bramble", "Swan Mustang", "Shy Warrior", "Moon Peaches", "Fuzzy Claws", "Red Delicious", "Sun Lemon", "Pitfall Whiskers", "High Heel Goddess", "Doll Champagne", "Trouble Mittens", "Show Boat", "Martini Flower", "Avenge Paws", "Cricket", "Sweet Manhattan", "Snout Pixie", "Pigeon Woman", "Dez Bonbon", "Wildcat Appaloosa", "Silver Agent", "Plum Moon", "Cougar Fuzz", "Alias Stick", "Swedish Twizz", "Pocket Muzzie", "Lady Peach", "Sugar Domino", "Wild Kitten", "Sky Trinity", "Morbid Sugar", "Slacker Cat", "Venom Charms", "Tall Honey", "Pepper Mouse", "Red Woman", "Maple Destroyer", "Friday Fox", "Sleek Zelda", "Wanted Candy", "Granola Dove", "Classy Luck", "Plenty Orange", "Purity Catnip", "Wonder Lady", "Tootsie Killer", "Bambi Wings", "HedgeH0g2", "ButterQuest", "Skittle Mine", "Bad Bunny", "Willow Dragon", "SmartieQuest", "Chip Queen", "Reed Lady", "DriftDetector", "Street Squirrel", "White Snare", "Night Magnet", "RoarSweetie", "Poppy Coffee", "Polar Bee", "Racy Lion", "Light Lion", "Subzero Taffy", "Chasm Face", "Mint Ness", "Pixie Soldier", "DuckDuck", "Mum Mary", "LifeRobber", "Killah Goose", "Daffy Girl", "Eerie Mizzen", "Acid Gosling", "Fennel Dove", "Jelly Camber", "Arsenic Coo", "Cool Iris", "Toy Dogwatch", "Roller Turtle", "Marigold Loot", "Vicious Street", "Alley Frog", "Moon Solitaire", "New York Winder", "Gullyway", "Snow Cream", "Trash Sling", "Romance Guppy", "SunVolt", "Green Scavenger", "Natural Gold", "SpellTansy", "Lava Nibbler", "Phoenix Tetra", "TulipCake", "Devil Blade", "Fire Fish", "Sienna Princess", "Twin Blaze", "Back Bett", "Bug Fire", "NoiseFire", "Koi Diva", "Widow Curio", "TrixiePhany", "Ember Rope", "Pink Hopper", "BlacKitten", "Congo Wire", "Betty Cricket", "Club Nola", "Impulsive Flower", "Dahlia Bumble", "Devil Chick", "Darling Peacock", "Reno Monarch", "Fire Feline", "Flame OUT", "Spooky Yellowjacket", "Nutmeg Riot", "RedMouth", "VenusLion", "NemesisX", "BloodEater", "Lunar Treat", "Feral Mayhem", "Terror Master", "Spunky Comet", "Fiend Oblivion", "Green Ghost", "Darkside Orbit", "Twinkle Cutlass", "Electric Saturn", "Metal Star", "Pearl Girl", "CoB@lt", "LunaStar", "Diamond Gamer", "Star Sword", "Cupid Dust", "Winter Bite", "Sass Burst", "MicroStar", "Fire Bite", "Mud Eye", "Starshine", "StormCake", "Cosmic Presence", "Digital Equinox", "Twister Hero", "Star Scratch", "RetroMirage", "Black Firefly", "Dakota Bliss", "Steel Solstice", "Blackfire", "Texas Butterfly", "Delirious Supernova", "Blizzard Vixen", "Geneva Cuffs", "Miss Twilight", "CirrusFlash", "Paris Boost", "StarZen", "PepperBurst", "London Fox", "Demo Zero", "Voodoo Cyclone", "Tokyo Dream", "Lucky Aurora", "Twisty Dew", "Dallas Burn", "Bang Shift", "Petite Flurry", "Nueva Nova", "Ginger Chaos", "Ship Whip", "Shady Prairie", "Murder Matter", "CloudFrenzy", "Milan Call Back", "FireBerry", "Pink Stream", "Roma Kabuki", "Light Despair", "SunnySnap", "Austin Shamrock", "Miss Nova", "Frosty Sunshine", "Athens Fire", "Parallax Sugar", "Aqua Monsoon", "Berlin Kamikaze", "Divine Quasar", "Breezy Mustang", "Bombay Shivers", "Virgo Moon", "Fuzzy Rainbow", "Kawaii Red", "Sun Leo", "Sand Whiskers", "California Goddess", "X-Dew", "Wiccan Thunder", "Cali Yacht", "Moon Radar", "Icy Avenger", "Lilac Lizard", "True Sweetness", "Snowflake Pixie", "Rosie Bird", "Dez North", "Jetta Talent", "Silver Rose", "Moon Laser", "Gold Bentley", "Daisy Stick", "Pixie Taze", "Pocket Mazda", "Black Fantastic", "Domino Combat", "Wild Tesla", "Sky Dahlia", "FLAK Angel", "Gothic Gucci", "Venom Petunia", "SWAT Honey", "Pepper Prada", "Lady Petal", "Helmet Destroyer", "Friday Ferrari", "Leaf Assassin", "Kevlar Wanted", "Dove Dolce", "Dance Bloom", "Orange Teflon", "Versace-Cat", "Lady Q", "Killer Grenade", "Bambi Benz",
		"Team-on",
		"Minereaster",
		"DimensionMan",
		"ChiKup",
		"el_conquistador",
	};

	public CanvasGroup canvasGroup;

	public TextMeshProUGUI ServerId;
	public TextMeshProUGUI[] PlayersText;
	public TextMeshProUGUI[] ScoresText;
	public TextMeshProUGUI NoConnection;

	public int UpdateKDSec = 30;
	DateTime lastUpdate;

	List<int> scores;
	List<string> players;

	private void Awake() {
		scores = new List<int>();
		players = new List<string>();
		lastUpdate = DateTime.Now.AddSeconds(-UpdateKDSec - 1);
	}

	public void Show() {
		ServerId.text = $"Server id: #{GameManager.Instance.Player.ServerId:D6}";
		UpdateLeaderboard();

		canvasGroup.alpha = 1.0f;
		canvasGroup.interactable = canvasGroup.blocksRaycasts = true;
	}

	public void Hide() {
		canvasGroup.alpha = 0.0f;
		canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
	}

	void UpdateLeaderboard() {
		if (IsConnectedToInternet())
			ShowFakePlayers();
		else
			ShowNoInternet();
	}

	bool IsConnectedToInternet() {
		if (Application.internetReachability == NetworkReachability.NotReachable)
			return false;

		string HtmlText = GetHtmlFromUri("http://google.com");
		if (HtmlText == "") {
			//no connection
			return false;
		}
		else if (!HtmlText.Contains("schema.org/WebPage")) {
			//Redirecting since the beginning of googles html contains that 
			//phrase and it was not found
			return false;
		}
		else {
			//success
			return true;
		}

		//ty, naglers
		//https://answers.unity.com/questions/567497/how-to-100-check-internet-availability.html?childToView=744803#answer-744803
		string GetHtmlFromUri(string resource) {
			string html = string.Empty;
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
			try {
				using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse()) {
					bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
					if (isSuccess) {
						using (StreamReader reader = new StreamReader(resp.GetResponseStream())) {
							//We are limiting the array to 80 so we don't have
							//to parse the entire html document feel free to 
							//adjust (probably stay under 300)
							char[] cs = new char[80];
							reader.Read(cs, 0, cs.Length);
							foreach (char ch in cs) {
								html += ch;
							}
						}
					}
				}
			}
			catch {
				return "";
			}
			return html;
		}
	}

	void ShowFakePlayers() {
		NoConnection.alpha = 0.0f;

		LoadArray();
		AddPlayerToArray();
		if((DateTime.Now - lastUpdate).TotalSeconds >= UpdateKDSec) {
			lastUpdate = DateTime.Now;
			FillArrayWithRandom();
		}
		SaveArray();

		for (byte i = 0; i < 10; ++i) {
			PlayersText[i].text = players[i];
			ScoresText[i].text = scores[i].ToString();
			PlayersText[i].alpha = ScoresText[i].alpha = 1.0f;
		}
	}

	void ShowNoInternet() {
		NoConnection.alpha = 1.0f;

		foreach (var player in PlayersText)
			player.alpha = 0.0f;
		foreach (var score in ScoresText)
			score.alpha = 0.0f;
	}

	void AddPlayerToArray() {
		int maxPlayerScore = PlayerPrefs.GetInt("maxScore", 100);
		if (players.Contains(GameManager.Instance.Player.Nickname)) {
			int i = players.IndexOf(GameManager.Instance.Player.Nickname);
			if (scores[i] != maxPlayerScore) {
				players.RemoveAt(i);
				scores.RemoveAt(i);
				players.Add(GameManager.Instance.Player.Nickname);
				scores.Add(maxPlayerScore);
			}
		}
		else {
			players.Add(GameManager.Instance.Player.Nickname);
			scores.Add(maxPlayerScore);
		}
	}

	void FillArrayWithRandom() {
		int maxPlayerScore = PlayerPrefs.GetInt("maxScore", 100);
		for (byte i = 0; i < 10; ++i) {
			players.Add(names[UnityEngine.Random.Range(0, names.Length - 1)]);
			scores.Add((int)((maxPlayerScore == 0 ? 100 : maxPlayerScore) * UnityEngine.Random.Range(0.5f, 1.2f)));
		}

		var playersArr = players.ToArray();
		var scoresArr = scores.ToArray();

		Array.Sort(scoresArr, playersArr);
		players.Clear();
		scores.Clear();
		players.AddRange(playersArr);
		scores.AddRange(scoresArr);
		players.Reverse();
		scores.Reverse();
		players.RemoveRange(10, players.Count - 10);
		scores.RemoveRange(10, scores.Count - 10);
	}

	void SaveArray() {
		PlayerPrefsX.SetStringArray("Leaderboard.players", players.ToArray());
		PlayerPrefsX.SetIntArray("Leaderboard.scores", scores.ToArray());
	}

	void LoadArray() {
		players.Clear();
		scores.Clear();
		players.AddRange(PlayerPrefsX.GetStringArray("Leaderboard.players"));
		scores.AddRange(PlayerPrefsX.GetIntArray("Leaderboard.scores"));
	}
}
